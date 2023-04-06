using System;
using System.Data.SqlClient;
using System.Data;

namespace dealer_m
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = DESKTOP-4AE5TIF\\ALWAN_FA; " +
                                    "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Hapus Data");
                                        Console.WriteLine("4. Mencari Data");
                                        Console.WriteLine("5. Ubah Data");
                                        Console.WriteLine("6. Keluar");
                                        Console.Write("\nEnter your choice (1-6): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DEALER MOBIL\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.WriteLine(" Masukkan ID : ");
                                                    string Id = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Pelanggan :");
                                                    string Nama = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jalan :");
                                                    string jln = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kota :");
                                                    string kt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kodepos :");
                                                    string kpos = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Id Mobil");
                                                    string idm = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(Id, Nama, jln, kt, kpos, idm, notlpn, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }

                                                }
                                                break;
                                            case '6':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From pembeli", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }
        public void insert(string id, string Nma, string jln, string kt, string kpos, string idm, string notlpn, SqlConnection con)
        {
            string str = "";
            str = "insert into pembeli (id, Nma, jln, kt, kpos, idm, notlp)values(@id_pembeli, @nama_pembeli, @jalan, @kota, @kode_pos, @id_mobil, @no_tlp)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id_pembeli", id));
            cmd.Parameters.Add(new SqlParameter("nama_pembeli", Nma));
            cmd.Parameters.Add(new SqlParameter("jalan", jln));
            cmd.Parameters.Add(new SqlParameter("kota", kt));
            cmd.Parameters.Add(new SqlParameter("kode_pos", kpos));
            cmd.Parameters.Add(new SqlParameter("id_mobil", idm));
            cmd.Parameters.Add(new SqlParameter("no_tlp", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
