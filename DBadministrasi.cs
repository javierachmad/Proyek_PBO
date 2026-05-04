using System;
using Npgsql;

class Database
{
    private NpgsqlConnection _koneksi;

    private string _connString =
        "Host=localhost;" +
        "Port=5432;" +
        "Database=Penjualan_spare_part;" +
        "Username=postgres;" +
        "Password=12345";

    public void Connect()
    {
        _koneksi = new NpgsqlConnection(_connString);
        _koneksi.Open();
        Console.WriteLine("✅ Berhasil terhubung ke database!");
    }

    public void Disconnect()
    {
        _koneksi?.Close();
        Console.WriteLine("🔌 Koneksi database ditutup.");
    }

    public NpgsqlConnection GetKoneksi() => _koneksi;
}