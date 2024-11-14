
namespace PizzaHurt.UI.FileHelper
{
    public class Filehelper : IFileHelper
    {
        IWebHostEnvironment _env;

        public Filehelper(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void DeleteFile(string imageUrl)
        {
            if (File.Exists(_env.WebRootPath + imageUrl))
            {
                File.Delete(_env.WebRootPath + imageUrl);
            }
        }

        public string uploadFile(IFormFile file)
        {
            var Upload = Path.Combine(_env.WebRootPath, "Images");
            bool exists = Directory.Exists(Upload);
            if (!exists) 
            Directory.CreateDirectory(Upload);

            var filename = GeneratedFileName(file.FileName);
            var fileStream = new FileStream(Path.Combine(Upload,filename),FileMode.Create);
            file.CopyToAsync(fileStream);

            return "/images/" + filename;
        }


        private string GeneratedFileName(string fileName) 
        { 
        string[] strname=fileName.Split('.');
        string strfilename=DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strname[strname.Length-1];
        return strfilename;
        }
    }
}
