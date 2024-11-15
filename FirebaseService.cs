using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using System;
using System.Threading.Tasks;

namespace cafeha
{
    public class FirebaseService
    {
        private FirestoreDb db;

        public FirebaseService()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("cafe-manager-c433e-firebase-adminsdk-ek0vv-9e78b5785f.json")
            });

            db = FirestoreDb.Create("cafe-manager");
            Console.WriteLine("Kết nối Firebase thành công.");
        }

        // Thêm đồ uống
        public async Task AddCafeItemAsync(CafeItem item)
        {
            DocumentReference docRef = await db.Collection("cafeItems").AddAsync(item);
            item.Id = docRef.Id; // Gán ID cho item
            Console.WriteLine($"Đồ uống {item.Name} đã được thêm với ID: {item.Id}");
        }

        // Lấy tất cả đồ uống
        public async Task<List<CafeItem>> GetCafeItemsAsync()
        {
            QuerySnapshot snapshot = await db.Collection("cafeItems").GetSnapshotAsync();
            List<CafeItem> items = new List<CafeItem>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                CafeItem item = document.ConvertTo<CafeItem>();
                item.Id = document.Id; // Lưu ID của tài liệu
                items.Add(item);
            }
            return items;
        }

        // Cập nhật đồ uống
        public async Task UpdateCafeItemAsync(CafeItem item)
        {
            DocumentReference docRef = db.Collection("cafeItems").Document(item.Id);
            await docRef.SetAsync(item, SetOptions.MergeAll);
            Console.WriteLine($"Đồ uống {item.Name} đã được cập nhật.");
        }

        // Xóa đồ uống
        public async Task DeleteCafeItemAsync(string itemId)
        {
            DocumentReference docRef = db.Collection("cafeItems").Document(itemId);
            await docRef.DeleteAsync();
            Console.WriteLine($"Đồ uống với ID {itemId} đã được xóa.");
        }
    }


