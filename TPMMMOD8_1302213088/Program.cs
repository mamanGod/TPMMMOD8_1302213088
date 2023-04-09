using System.Text.Json;

public class Covid
{
    public string satuanSuhu { get; set; }
    public int batasHariDemam { get; set; }
    public string pesanDitolak { get; set; }
    public string pesanDiterima { get; set; }

    public Covid(string satuanSuhu, int batasHariDemam, string pesanDitolak, string pesanDiterima)
    {
        this.satuanSuhu = satuanSuhu;
        this.batasHariDemam = batasHariDemam;
        this.pesanDitolak = pesanDitolak;
        this.pesanDiterima = pesanDiterima;
    }

    public Covid() { }
}

public class CovidConfig
{
    public Covid config;
    public const string fileLocation = "G:\\TP KPL\\TPMOD8_1302213088\\TPMOD8_1302213088\\TPMOD8_1302213088";
    public const string fiilePath = fileLocation + "config.json";

    public CovidConfig()
    {
        try
        {
            readConfigFile();
        }
        catch
        {
            setDefault();
            writeCovidConfig();
        }
    }
    private Covid readConfigFile()
    {
        string configJsonData = File.ReadAllText(fiilePath);
        config = JsonSerializer.Deserialize<Covid>(configJsonData);
        return config;
    }


    public void setDefault()
    {
        string CONFIG1 = "celcius";
        int CONFIG2 = 14;
        string CONFIG3 = "Anda tidak boleh masuk ke dalam gedung";
        string CONFIG4 = "Anda boleh masuk ke dalam gedung";
        config = new Covid(CONFIG1, CONFIG2, CONFIG3, CONFIG4);
    }

    private void writeCovidConfig()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        string jsonString = JsonSerializer.Serialize(config, options);
        File.WriteAllText(fiilePath, jsonString);
    }

    public void ubahSatuan()
    {
        if (config.satuanSuhu == "celcius")
        {
            config.satuanSuhu = "fahrenheit";
        }
        else
        {
            config.satuanSuhu = "celcius";
        }
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        CovidConfig covidConfig = new CovidConfig();

        Console.WriteLine("Berapa suhu badan kamu saat ini ? dalam nilai " + covidConfig.config.satuanSuhu);
        double suhuBadan = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Berapa hari yang lalu apa ada gejala demam?");
        int terakhirDemam = Convert.ToInt32(Console.ReadLine());

        if (covidConfig.config.satuanSuhu == "celcius")
        {
            string pesan = suhuBadan >= 36.5 && suhuBadan <= 37.5 && terakhirDemam < covidConfig.config.batasHariDemam ?
                covidConfig.config.pesanDiterima : covidConfig.config.pesanDitolak;
            Console.WriteLine(pesan);
        }
        else if (covidConfig.config.satuanSuhu == "fahrenheit")
        {
            string pesan = suhuBadan >= 97.7 && suhuBadan <= 99.5 && terakhirDemam < covidConfig.config.batasHariDemam ?
                covidConfig.config.pesanDiterima : covidConfig.config.pesanDitolak;
        }
    }
}