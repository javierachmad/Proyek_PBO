class Program
{
    static void Main()
    {
    // --- Produk ---
    var ssd = new Sparepart("STR-001", "SSD 512GB Samsung", 850000, 20, "Storage", "Samsung");
    var ram = new ProdukBekas("MEM-001", "RAM 8GB DDR4 Bekas", 450000, 5, "Baik", 30);

    Console.WriteLine($"{ssd.NamaProduk} → Rp {ssd.HitungHarga():N0}");
    Console.WriteLine($"{ram.NamaProduk} → Rp {ram.HitungHarga():N0} (diskon 30%)");

    // --- Pelanggan & Diskon (Polimorfisme) ---
    Pelanggan budi = new PelangganReguler("Budi", "Jember", 08111, 1500000);
    Pelanggan sari = new PelangganVIP("Sari", "Jember", 08222, 1500000);

    Console.WriteLine($"\n{budi.GetInfo()} → Diskon: Rp {budi.HitungDiskon():N0}");
    Console.WriteLine($"{sari.GetInfo()} → Diskon: Rp {sari.HitungDiskon():N0}");

    // --- Penjualan ---
    var jual = new Penjualan(idPelanggan: 1, idKaryawan: 1);
    jual.TambahDetail(new DetailPenjualan("SSD 512GB Samsung", 1, 850000));
    jual.TambahDetail(new DetailPenjualan("RAM 8GB DDR4", 2, 315000));
    jual.SetDiskon((double)sari.HitungDiskon());
    jual.CetakNota();

    // --- Tiket Servis ---
    var teknisi = new Teknisi("Andi", "Jember", 08333, "Komputer", "08.00-17.00");
    var tiket = new TiketServis(idPelanggan: 1, teknisi, "Laptop tidak menyala");

    tiket.SetDiagnosa("Kerusakan pada RAM dan thermal paste kering");
    tiket.SetBiayaJasa(75000);
    tiket.TambahDetailServis(new DetailServis("RAM 8GB DDR4", 1, 450000));
    tiket.UpdateStatus("selesai");
    tiket.CetakTiket();
    }
}
