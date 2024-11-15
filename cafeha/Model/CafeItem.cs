using Google.Cloud.Firestore;

namespace cafeha.Model
{
    [FirestoreData] // Đánh dấu rằng đây là một lớp Firestore
    public class CafeItem
    {
        [FirestoreDocumentId] // Đánh dấu ID của tài liệu
        public string Id { get; set; }

        [FirestoreProperty] // Thuộc tính sẽ được ánh xạ vào Firestore
        public string Name { get; set; }

        [FirestoreProperty]
        public double Price { get; set; }

        [FirestoreProperty]
        public string ImageUrl { get; set; }

        [FirestoreProperty]
        public string Category { get; set; }


    }
}
