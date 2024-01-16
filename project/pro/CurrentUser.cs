using pro.Models;

namespace pro
{
    public static class CurrentUser
    {
        static public string name = String.Empty;
        static public string surname = String.Empty;
        static public string username = String.Empty;
        static public string phonenumber = String.Empty;
        static public int admin;

        public static void ClearUserData()
        {
            name = String.Empty;
            surname = String.Empty;
            username = String.Empty;
            phonenumber = String.Empty;
            admin = 0;
        }

        public static bool IsUserAuthenticated()
        {
            if (CurrentUser.username == String.Empty) 
            {
                return false;
            }
            return true;
        }
    }
}
