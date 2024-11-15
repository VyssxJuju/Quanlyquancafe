using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using cafeha.Model;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace cafeha.Controller
{
    public class FirebaseService
    {
        private FirestoreDb db;
        private static bool _isFirebaseInitialized = false;

        public FirebaseService()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            if (!_isFirebaseInitialized)
            {
                string path = @"D:\code\btl_quanlyquancafe\cafeha\cafeha\bin\Debug\net8.0-windows\cafe-manager-c433e-firebase-adminsdk-ek0vv-9e78b5785f.json";

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                });

                db = FirestoreDb.Create("cafe-manager-c433e");
                _isFirebaseInitialized = true;
                Console.WriteLine("Kết nối Firebase thành công.");
            }
            else
            {
                db = FirestoreDb.Create("cafe-manager-c433e");
            }
        }

        // Đăng nhập người dùng
        public async Task<bool> LoginUser(string username, string password)
        {
            // Kiểm tra thông tin đăng nhập từ Firestore
            var userSnapshot = await db.Collection("Users").WhereEqualTo("username", username).WhereEqualTo("password", password).GetSnapshotAsync();

            if (userSnapshot.Documents.Count == 0)
            {
                return false; // Người dùng không tồn tại hoặc thông tin đăng nhập không đúng
            }

            // Người dùng đã xác thực thành công
            var userDocument = userSnapshot.Documents.First();

            // Tạo token (giả sử đây là chuỗi ngẫu nhiên đơn giản, bạn có thể sử dụng JWT hoặc cách khác)
            string userToken = Guid.NewGuid().ToString(); // Tạo token ngẫu nhiên

            // Cập nhật token vào Firestore cho người dùng này
            var userRef = userDocument.Reference;
            await userRef.UpdateAsync(new Dictionary<string, object>
                {
                    { "token", userToken }
                });

            // Lưu token vào biến hoặc trả về token cho client
            CurrentUserToken = userToken; // Giả sử bạn có biến lưu token người dùng hiện tại

            return true; // Đăng nhập thành công
        }

        public string CurrentUserToken { get; private set; } // Biến lưu token hiện tại




        // Đăng ký người dùng
        public async Task<string> RegisterUser(string username, string password, string role = null)
        {
            // Kiểm tra username đã tồn tại chưa
            Query query = db.Collection("Users").WhereEqualTo("username", username);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count > 0)
            {
                throw new Exception("Username đã tồn tại.");
            }

            DocumentReference docRef = db.Collection("Users").Document();
            string token = Guid.NewGuid().ToString(); // Tạo token đơn giản

            Dictionary<string, object> userData = new Dictionary<string, object>
                {
                    { "username", username },
                    { "password", password },
                    { "role", role ?? "none" },
                    { "token", token } // Lưu token
                };
            await docRef.SetAsync(userData);

            return docRef.Id; // Trả về UID của người dùng
        }


        public async Task<string> GetUserTokenAsync(string username)
        {
            try
            {
                var userSnapshot = await db.Collection("Users")
                    .WhereEqualTo("username", username)
                    .GetSnapshotAsync();

                if (userSnapshot.Documents.Count > 0)
                {
                    // Lấy document đầu tiên (nếu có)
                    var userDoc = userSnapshot.Documents[0];
                    return userDoc.GetValue<string>("token");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy token người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null; // Trả về null nếu không tìm thấy
        }

        public async Task<bool> IsUserTokenValid(string token)
        {
            var userSnapshot = await db.Collection("Users").WhereEqualTo("token", token).GetSnapshotAsync();

            if (userSnapshot.Documents.Count == 0)
            {
                return false; // Token không hợp lệ nếu không có người dùng nào có token này
            }

            return true; // Token hợp lệ
        }

        // Lưu vai trò người dùng
        public async Task SetUserRoleAsync(string uid, string role = null)
        {
            DocumentReference docRef = db.Collection("Users").Document(uid);
            if (role != null)
            {
                await docRef.SetAsync(new { role = role }, SetOptions.MergeAll);
            }
        }

        public async Task<bool> CheckUserTokenAsync(string token)
        {
            try
            {
                var userSnapshot = await db.Collection("Users")
                    .WhereEqualTo("token", token)
                    .GetSnapshotAsync();

                return userSnapshot.Documents.Count > 0; // Trả về true nếu tìm thấy token
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra token: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; // Trả về false nếu có lỗi
            }
        }

        // Lấy vai trò người dùng
        public async Task<string> GetUserRoleAsync(string username)
        {
            try
            {
                var userSnapshot = await db.Collection("Users")
                    .WhereEqualTo("username", username)
                    .GetSnapshotAsync();

                if (userSnapshot.Documents.Count > 0)
                {
                    // Lấy document đầu tiên (nếu có)
                    var userDoc = userSnapshot.Documents[0];
                    return userDoc.GetValue<string>("role");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy vai trò người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null; // Trả về null nếu không tìm thấy
        }



        // Thêm đồ uống
        public async Task AddCafeItemAsync(CafeItem item)
        {
            DocumentReference docRef = await db.Collection("CafeItems").AddAsync(item);
            item.Id = docRef.Id;
            Console.WriteLine($"Đồ uống {item.Name} đã được thêm với ID: {item.Id}");
        }

        // Lấy tất cả đồ uống
        public async Task<List<CafeItem>> GetCafeItemsAsync()
        {
            QuerySnapshot snapshot = await db.Collection("CafeItems").GetSnapshotAsync();
            List<CafeItem> items = new List<CafeItem>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                CafeItem item = document.ConvertTo<CafeItem>();
                item.Id = document.Id;
                items.Add(item);
            }
            return items;
        }

        // Cập nhật đồ uống
        public async Task UpdateCafeItemAsync(CafeItem item)
        {
            DocumentReference docRef = db.Collection("CafeItems").Document(item.Id);
            await docRef.SetAsync(item, SetOptions.MergeAll);
            Console.WriteLine($"Đồ uống {item.Name} đã được cập nhật.");
        }

        public async Task SaveOrderToDatabase(Order order)
        {
            var orderCollection = db.Collection("Orders");
            await orderCollection.AddAsync(order);
            Console.WriteLine($"Đơn hàng cho {order.CustomerName} đã được thêm với đồ uống: {order.DrinkName}");
        }

        // Xóa đồ uống
        public async Task DeleteCafeItemAsync(string itemId)
        {
            DocumentReference docRef = db.Collection("CafeItems").Document(itemId);
            await docRef.DeleteAsync();
            Console.WriteLine($"Đồ uống với ID {itemId} đã được xóa.");
        }
    }
}
