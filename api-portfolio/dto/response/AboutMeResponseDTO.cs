namespace api_portafolio.dto.response
{
    public class AboutMeResponseDTO 
    {
        private long id;
        public long Id 
        {
            get
            {
                return this.id;
            } 
            set
            {
                this.id = value;
            }
        }
        private string aboutMe;
        public string AboutMe
        { 
            get
            {
                return this.aboutMe;
            }
            set
            {
                this.aboutMe = value;
            }
        }
        private string profile_photo;
        public string Profile_photo 
        { 
            get
            {
                return this.profile_photo;
            } 
            set
            {
                this.profile_photo = value;
            }
        }
        private List<string> technologies;
        public List<string> Technologies 
        {
            get
            {
                return this.technologies;
            }
            set
            {
                this.technologies = value;
            } 
        }
        private List<string> job_skills;
        public List<string> Job_skills
        {
            get
            {
                return this.job_skills;
            }
            set
            {
                this.job_skills = value;
            }
        }

    }
}