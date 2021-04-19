using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestAPI_BTTH.Controllers
{
    public class SanPhamController : ApiController
    {
        SanPhamDataContext sp = new SanPhamDataContext();
        [HttpGet]
        public List<SanPham> GetSanPhamLists()
        {
            return sp.SanPhams.ToList();
        }
        //2. Dịch vụ lấy thông tin một khách hàng với mã nào đó
        [HttpGet]
        public IEnumerable<SanPham> GetSanPham(string id)
        {
            return sp.SanPhams.Where(n => n.MaSP.Contains(id)).ToList();
        }
        [Route("api/{ControllerName}/getbyname")]
        [HttpGet]
        public IEnumerable<SanPham> GetSanPhamByName(string name)
        {
            return sp.SanPhams.Where(x => x.TenSP.Contains(name));
        }
        [Route("api/{ControllerName}/tonkho")]
        [HttpGet]
        public IEnumerable<SanPham> GetSanPhamTonKho()
        {
            return sp.SanPhams.Where(x => x.SoLuong > 0);
        }
        //3. httpPost, dịch vụ thêm mới một khách hàng
        [HttpPost]
        public bool InsertNewSanPham(string id, string name, string mota, string gianhap, string giaban, int soluong)
        {
            try
            {
                SanPham customer = new SanPham();
                customer.MaSP = id;
                customer.TenSP = name;
                customer.MoTa = mota;
                customer.GiaNhap = gianhap;
                customer.GiaBan = giaban;
                customer.SoLuong = soluong;
                sp.SanPhams.InsertOnSubmit(customer);
                sp.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //4. httpPut để chỉnh sửa thông tin một khách hàng
        [HttpPut]
        public bool UpdateSanPham(string id, string name, string mota, string gianhap, string giaban, int soluong)
        {
            try
            {
                //Lấy mã khách đã có
                SanPham customer = sp.SanPhams.FirstOrDefault(x => x.MaSP == id);
                if (customer == null) return false;
                customer.TenSP = name;
                customer.MoTa = mota;
                customer.GiaNhap = gianhap;
                customer.GiaBan = giaban;
                customer.SoLuong = soluong;
                sp.SubmitChanges();//Xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }
        //5.httpDelete để xóa một Khách hàng
        [HttpDelete]
        public bool DeleteCustomer(string id)
        {
            try
            {
                //Lấy mã khách đã có
                SanPham customer = sp.SanPhams.FirstOrDefault(x => x.MaSP == id);
                if (customer == null) return false;
                sp.SanPhams.DeleteOnSubmit(customer);
                sp.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}