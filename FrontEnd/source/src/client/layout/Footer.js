import React from "react";
import { Link } from "react-router-dom";

const Footer = (props) => {
  const { policies, generalInfomations, socialMedias } = props.data;
  return (
    <div className="footer p-5 bg-black">
      <div className="p-5 grid grid-cols-4 mx-auto max-w-[1700px] container:none ">
        <div className="py-5">
          <img
            className="float-left" 
            src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2016/07/15213842/logo-full-white.png"
            alt=""
          />
          <p className="text-white">
            <strong>CÔNG TY CỔ PHẦN THỜI TRANG QUỐC TẾ GLAMOR</strong>
            <br />
            <strong>VPGD:</strong> Tầng 8 tòa nhà SANNAM, 78 Duy Tân, Q.Cầu
            Giấy, Hà Nội.
            <br />
            <strong>Số giấy chứng nhận ĐKKD:</strong>
            0107370211
            <br />
            <strong>Nơi cấp:</strong>Sở Kế hoạch và Đầu tư thành phố Hà Nội
            <br /> <strong>Ngày cấp:</strong> 24/03/2016
            <br />
            <br />
            <strong>Liên hệ hợp tác: </strong>
            <a href="tel:0983377038">098.3377.038</a>
            <br />
            <strong>
              Nhượng quyền thương hiệu/ Đặt quà tặng số lượng lớn:
            </strong>
            <a href="tel:0985219048">0985.219.048</a>
            <br />
            <span
              style={{ color: "#ff0000" }}
              data-gtm-vis-recent-on-screen9417962_148={35068}
              data-gtm-vis-first-on-screen9417962_148={35068}
              data-gtm-vis-total-visible-time9417962_148={1000}
              data-gtm-vis-has-fired9417962_148={1}
            >
              <strong>
                TỔNG ĐÀI BÁN HÀNG (08H - 17H):
                <br />
              </strong>
              <a href="tel:0966353000">0966.353.000</a> -{" "}
              <a href="tel:0906636000">0906.636.000</a>
            </span>
            <br />
            <strong>Email:</strong> cskh@elly.vn
            <br /> <strong>Website</strong>: https://elly.vn
          </p>
          <Link>
            <img
              className="w-52 my-5 "
              src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/07/13173902/logoSaleNoti.png"
              alt=""
            />
          </Link>
        </div>
        <div className="policies-regulations p-5">
          <p className="text-white first-line:font-bold first-line:uppercase">
            Chính sách và quy định
          </p>
          <div className="border-t-2 w-5 my-5"></div>
          {policies.map((item) => {
            return (
              <li
                key={item.id}
                className={`my-5 flex-none list-none text-white border-b border-gray-500 cursor-pointer`}
              >
              <Link to={item.url}>{item.title} </Link>
              </li>
            );
          })}
        </div>
        <div className="general-information p-5">
          <p className="text-white first-line:font-bold first-line:uppercase">
            Thông tin chung
          </p>
          <div className="border-t-2 w-5 my-5"></div>
          {generalInfomations.map((item) => {
            return (
              <li
                key={item.id}
                className={`my-5 flex-none list-none border-b border-gray-500 text-white cursor-pointer`}
              >
              <Link to={item.url}>{item.title} </Link>
              </li>
            );
          })}
          
        </div>
        <div className="fan-page p-5">
          <p className="text-white first-line:font-bold first-line:uppercase">
            Theo dõi chúng tôi
          </p>
          <div className="border-t-2 w-5 my-5"></div>
          {socialMedias.map((item) => {
            return (
              <li
                key={item.id}
                className={`my-5 flex list-none text-white border-b border-gray-500 cursor-pointer`}
              >
                <img className="w-6 h-6" src={item.picture}/>
 
                <p className="mx-2">{item.name}</p>
              </li>
            );
          })}
        </div>
      </div>
    </div>
  );
};

export default Footer;