﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using HotelManager.Data;
using HotelManager.Data.Entity;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HotelManager.Business
{
    class BusPhong
    {
        /// <summary>
        /// Xử lý tác vụ liên quan tới PHONG
        /// </summary>
        public static DataTable LoadExcelFile(string path)
        {
            string connstr = null;
            if (path.EndsWith(".xls"))
            {
                connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source='" + path + "';Extended Properties=Excel 8.0";
            }

            if (connstr != null)
            {
                OleDbConnection conn = new OleDbConnection(connstr);
                string strSQL = "SELECT * FROM [Sheet1$]";

                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0];
            }

            return null;
        }


        public static ArrayList GetList()
        {
            return DataPhong.GetList();
        }

        /// <summary>
        /// Lấy hết bảng PHONG từ database
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTable()
        {
            return DataPhong.GetTable();
        }


        /// <summary>
        /// Thêm phòng từ dữ liệu của 1 DataGridViewRowCollection
        /// </summary>
        /// <returns>Trả về số Phòng được thêm</returns>
        public static int ThemPhong(DataGridViewRowCollection rows)
        {
            int count = rows.Count - 1;
            for (int i = 0; i < count; ++i)
            {
                DataGridViewRow row = rows[i];
                Phong phong = new Phong();

                phong.TenPhong = "" + row.Cells[0].Value;
                phong.MaLoaiPhong = Int16.Parse(row.Cells[1].Value.ToString());
                phong.TinhTrangHienTai = true;
                phong.MoTa = "" + row.Cells[2].Value;

                DataPhong.AddPhong(phong);
            }

            return count;
        }

         /// <summary>
        /// Thêm một phòng
        /// </summary>
        /// <param name="phong"></param>
        /// <returns></returns>
        public static bool AddPhong(Phong phong)
        {
            return DataPhong.AddPhong(phong);
        }

        /// <summary>
        /// Cập nhật thông tin 1 phòng
        /// </summary>
        /// <param name="phong"></param>
        public static void UpdatePhong(Phong phong)
        {
            DataPhong.UpdatePhong(phong);
        }

        /// <summary>
        /// Xóa một phòng
        /// </summary>
        /// <param name="maPhong"></param>
        public static void DeletePhong(int maPhong)
        {
            DataPhong.DeletePhong(maPhong);
        }

        /// <summary>
        /// Tìm kiếm 1 phòng thông qua MaPhong
        /// </summary>
        /// <param name="_maPhong"></param>
        /// <returns>Phong</returns>
        public static Phong FindMaPhong(int _maPhong)
        {
            return DataPhong.FindMaPhong(_maPhong);
        }

        /// <summary>
        /// Tìm kiếm 1 phòng thông qua TenPhong
        /// </summary>
        /// <param name="_tenPhong"></param>
        /// <returns>Phong</returns>
        public static Phong FindTheoTenPhong(String _tenPhong)
        {
            return DataPhong.FindTheoTenPhong(_tenPhong);
        }

        /// <summary>
        /// Tim kiem theo Mã Loại Phòng
        /// </summary>
        public static DataTable FindMaLoaiPhong(int _maLoaiPhong)
        {
            return DataPhong.FindMaLoaiPhong(_maLoaiPhong);
        }
        
        /// <summary>
        /// Tim kiem theo tinh trang
        /// </summary>
        public static DataTable FindTinhTrang(Boolean _tinhTrang)
        {
            return DataPhong.FindTinhTrang(_tinhTrang);
        }

        /// <summary>
        /// Tim kiem bang Mô tả
        /// </summary>
        public static DataTable FindGhiChu(string _moTa)
        {
            return DataPhong.FindGhiChu(_moTa);
        }

        //Phan them vao

        public static DataTable Timphongtheotenloaiphong(String tenloaiphong, DateTime _thoidiemden, DateTime _thoidiemdi)
        {
            return DataPhong.LayPhongTrongTheoTenloaiPhong(tenloaiphong, true, _thoidiemden, _thoidiemdi);
        }

        public static DataTable Timphongtheothoidiem(DateTime _thoidiemden, DateTime _thoidiemdi)
        {
            return DataPhong.Timphongtheothoidiem(_thoidiemden, _thoidiemdi, true);
        }

        public static PhieuDatCho FindSDT(String _SDT)
        {
            return DataPhieuDatCho.FindTheoSDT(_SDT);
        }

        //Phan them vao

        public static DataTable layphongchothue(bool tt)
        {
            return DataPhong.GetPhongtrongchothue(tt);
        }
        public static DataTable LayDanhSachPhongDaDat(String tendat, String sdt)
        {
            return DataPhong.GetPhongDaDat(tendat, sdt);
        }

        public static DataTable layphongtrongtheoloaiphong(String loaiphong)
        {
            return DataPhong.layphongtrongtheoloaiphong(loaiphong);
        }

        /// <summary>
        /// Xác nhận thuộc tính Tình trạng phòng là còn trống (do trả phòng)
        /// </summary>
        /// <param name="MaPhong"></param>
        public static void TraPhong(int MaPhong)
        {
            DataPhong.TraPhong(MaPhong);
        }

        /// <summary>
        /// Xác nhận là phòng đã được thuê (ko còn trống --> Tình trạng phòng == false)
        /// </summary>
        /// <param name="MaPhong"></param>
        public static void ChoKhachNhanPhong(int MaPhong)
        {
            DataPhong.ChoKhachNhanPhong(MaPhong);
        }
    }
}
