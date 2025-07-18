using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class SystemSettingViewModel : BaseEntity
    {
        public int SystemSettingId { get; set; }

        public string SystemSettingLogoImageUrl { get; set; }

        public string SystemSettingLogoImageUrl2 { get; set; }

        public string SystemSettingCopyright { get; set; }

        public string SystemSettingWelcomeNoteTitle { get; set; }

        public string SystemSettingWelcomeNoteBreef { get; set; }

        public string SystemSettingWelcomeNoteDesc { get; set; }

        public string SystemSettingWelcomeNoteUrl { get; set; }

        public string SystemSettingWelcomeNoteImageUrl { get; set; }

        public string SystemSettingMapLocation { get; set; }

        public IFormFile SystemSettingFile1 { get; set; }

        public IFormFile SystemSettingFile2 { get; set; }

        public IFormFile SystemSettingFile3 { get; set; }
    }
}
