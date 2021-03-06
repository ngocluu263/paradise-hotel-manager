﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MySql.Data.MySqlClient;
using HotelManager.Data.Entity;
using System.Data;
using System.Windows.Forms;

namespace HotelManager.Data
{
    class DataPhieuThu
    {
        public static ArrayList GetList()
        {
            ArrayList listPhieuThu = new ArrayList();
            string StrSQL = "SELECT * FROM phieu_thu";
            MySqlCommand ObjCmd = DataProvider.getInstance().getCommand();
            ObjCmd.CommandText = StrSQL;
            MySqlDataReader ObjReader;
            ObjReader = ObjCmd.ExecuteReader();

            while (ObjReader.Read())
            {
                PhieuThu phieuthu = new PhieuThu();
                
                phieuthu.MaPhieuThu = (int)ObjReader["MaPhieuThu"];
                phieuthu.TenKhach = (string)ObjReader["TenKhach"];
                phieuthu.CMND = (string)ObjReader["CMND"];
                phieuthu.MaNhanVien = (int)ObjReader["MaNhanVien"];
                phieuthu.ThoiDiemThu = (DateTime)ObjReader["ThoiDiemThu"];
                phieuthu.TongTienThu = (float)ObjReader["TongTienThu"];

                listPhieuThu.Add(phieuthu);
            }

            //close connection
            DataProvider.getInstance().CloseConnection();

            return listPhieuThu;
        }

        public static DataTable GetTable()
        {
            DataTable table = new DataTable();
            string StrSQL = "SELECT * FROM phieu_thu";
            MySqlDataAdapter ObjAdapter = new MySqlDataAdapter(StrSQL, DataProvider.getInstance().getConnection());
            ObjAdapter.Fill(table);

            //close connection
            DataProvider.getInstance().CloseConnection();

            return table;
        }

        public static void UpdateTable(DataTable dataTable)
        {
            //tao chuoi ket noi.
            MySqlConnection ObjCn = DataProvider.getInstance().getConnection();
            string StrSQL = "SELECT * FROM phieu_thu";

            MySqlDataAdapter ObjAdapter = new MySqlDataAdapter(StrSQL, ObjCn);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(ObjAdapter);

            ObjAdapter.Update(dataTable);

            //close connection
            DataProvider.getInstance().CloseConnection();

        }

        public static void UpdatePhieuThu(PhieuThu phieuthu)
        {
            string StrSQL = "UPDATE phieu_thu SET TenKhach = " + phieuthu.TenKhach
                + ", CMND = " + phieuthu.CMND
                + ", MaNhanVien = " + phieuthu.MaNhanVien
                + ", ThoiDiemThu = " + phieuthu.ThoiDiemThu
                + ", TongTienThu = " + phieuthu.TongTienThu
                + " WHERE MaPhieuThu = " + phieuthu.MaPhieuThu;


            MySqlCommand ObjCmd = DataProvider.getInstance().getCommand();
            ObjCmd.CommandText = StrSQL;





            ObjCmd.Parameters.Add("@MaPhieuThu", MySqlDbType.Int32).Value = phieuthu.MaPhieuThu;

            ObjCmd.ExecuteNonQuery();

            //close connection
            DataProvider.getInstance().CloseConnection();

        }

        public static bool Add(PhieuThu phieuthu)
        {
            try
            {
                MySqlConnection ObjCn = DataProvider.getInstance().getConnection();

                string StrSQL = "INSERT INTO phieu_thu(TenKhach, CMND, MaNhanVien, ThoiDiemThu, TongTienThu)" 
                    + " VALUES(" 
                    + phieuthu.TenKhach + ","
                    + phieuthu.CMND + ","
                    + phieuthu.MaNhanVien + ","
                    + phieuthu.ThoiDiemThu + ","
                    + phieuthu.TongTienThu + ");";


                MySqlCommand ObjCmd = new MySqlCommand(StrSQL, ObjCn);






                ObjCmd.ExecuteNonQuery();

                //Theo bạn Hiệp nghĩ là để update MaPhong theo TenPhong, ~ tăng cái primary key
                StrSQL = "Select @@IDENTITY";

                ObjCmd = new MySqlCommand(StrSQL, ObjCn);
                phieuthu.MaNhanVien = (int)ObjCmd.ExecuteScalar();

                //close connection
                DataProvider.getInstance().CloseConnection();

                return true;
            }
            catch (Exception ee)
            {
                if (ee.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Dữ liệu trùng lặp: PhieuThu " + phieuthu.TenKhach);
                }

                //close connection
                DataProvider.getInstance().CloseConnection();

                return false;
            }
        }

        public static void Delete(int maPhieuThu)
        {
            string StrSQL = "DELETE FROM phieu_thu WHERE MaPhieuThu = " + maPhieuThu;

            MySqlCommand ObjCmd = DataProvider.getInstance().getCommand();
            ObjCmd.CommandText = StrSQL;



            ObjCmd.ExecuteNonQuery();

            //close connection
            DataProvider.getInstance().CloseConnection();

        }

        public static DataTable Find(int maPhieuThu)
        {
            DataTable dt = new DataTable();

            string StrSQL = "SELECT * FROM phieu_thu WHERE MaPhieuThu = " + maPhieuThu;
            MySqlCommand ObjCmd = DataProvider.getInstance().getCommand();
            ObjCmd.CommandText = StrSQL;



            MySqlDataAdapter adapter = new MySqlDataAdapter(ObjCmd);
            adapter.Fill(dt);

            //close connection
            DataProvider.getInstance().CloseConnection();

            return dt;
        }

        public static DataTable Find(string tenKhach)
        {
            DataTable dt = new DataTable();

            string StrSQL = "SELECT * FROM phieu_thu WHERE TenKhach = " + tenKhach;

            MySqlCommand ObjCmd = DataProvider.getInstance().getCommand();
            ObjCmd.CommandText = StrSQL;



            MySqlDataAdapter adapter = new MySqlDataAdapter(ObjCmd);
            adapter.Fill(dt);

            //close connection
            DataProvider.getInstance().CloseConnection();

            return dt;
        }

    }
}
