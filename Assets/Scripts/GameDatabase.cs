using System.Collections;

public class GameDatabase {
    private static GameDatabase s_GameDatabase;

    public int money;
    public int time;
    public int experience;
    public int hunger;
    public int thirst;
    public int energy;
    public int psychology = 1;
    public int health = 1;
    public int level = 1;
    public string token;
    public string name;
    public bool isFirstStart = true;
    public int drama = 1;
    public string userID;

    private bool m_IsInit = false;
    private bool m_NeedSync = false;

    private GameDatabase() { }

    public static GameDatabase GetInstance()
    {
        if (s_GameDatabase == null)
        {
            s_GameDatabase = new GameDatabase();
        }
        if (!s_GameDatabase.m_IsInit)
        {
            s_GameDatabase.Init();
            s_GameDatabase.m_IsInit = true;
        }
        return s_GameDatabase;
    }

    private void Init()
    {
        money = 80;
    }

    private bool Sync()
    {
        bool ret = false;
        return ret;
    }
}