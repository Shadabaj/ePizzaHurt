namespace PizzaHurt.UI.FileHelper
{
    public interface IFileHelper
    {


        void DeleteFile (string imageUrl);

        string uploadFile(IFormFile file);

    }
}
