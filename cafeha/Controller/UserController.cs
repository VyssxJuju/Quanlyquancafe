using cafeha.Controller;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;

public class UserController
{
    private readonly FirebaseService _firebaseService;

    public UserController()
    {
        _firebaseService = new FirebaseService();
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
    {
        var storage = new FirebaseStorage("your_storage_bucket"); // Thay "your_storage_bucket" bằng URL bucket của bạn

        var imageUrl = await storage
            .Child("profile_images") // Thư mục chứa ảnh
            .Child(fileName)
            .PutAsync(imageStream);

        return imageUrl; // Trả về URL của ảnh đã tải lên
    }

    public async Task SaveUserAsync(User user, Stream imageStream)
    {
        // Lưu ảnh và nhận URL
        if (imageStream != null)
        {
            string fileName = $"{user.Username}_{Guid.NewGuid()}.jpg"; // Tạo tên file duy nhất
            user.ProfileImageUrl = await UploadImageAsync(imageStream, fileName);
        }

        //await _firebaseService.SaveUserToDatabase(user); // Lưu thông tin người dùng vào Firestore             
    }
}
