import React, { useState } from "react";
import HtmlContent from "../../../../components/HtmlContent";
import Rating from "./Rating";
import BuyGuide from "./BuyGuide";
import RefundPolicy from "./RefundPolicy";
import SizeGuide from "./SizeGuide";
import { TEXT_CONSTANTS, Title_Multitap_List } from "../Constants/clientPageConstants";

const MultiTaps = ({ data }) => {
  const brach = data.branchList;
  const [activeTab, setActiveTab] = useState("Mô Tả");

  const groupedByRegion = brach.reduce((acc, item) => {
    // Nếu nhóm chưa có, tạo một nhóm mới
    if (!acc[item.region]) {
      acc[item.region] = [];
    }
    // Thêm phần tử vào nhóm
    acc[item.region].push(item);
    return acc;
  }, {});

  const convertBranchObjectToArray = Object.entries(groupedByRegion).map(
    ([key, value]) => ({
      region: key,
      data: value,
    })
  );

  return (
    <div className="MultiTaps ">
      <div className="container ">
        <div className="tabs border-2">
          <div className="tab-list flex border box-border ">
            {/* Tabs Navigation */}
            <ul
              className="w-full justify-around flex text-sm font-medium text-center text-gray-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400"
              role="tablist"
            >
              {Title_Multitap_List &&
                Title_Multitap_List.map((item,index) => (
                  <li className="w-full -mt-[2px]" role="presentation" key={index}>
                    <button
                      className={`inline-block w-full p-4 border-t-2 bg-gray-100 ${
                        activeTab === item.title
                          ? "text-black border-red-600 bg-white"
                          : "border-transparent hover:text-gray-600 hover:border-gray-300"
                      }`}
                      onClick={() => setActiveTab(item.title)}
                      type="button"
                      role="tab"
                    >
                      {item.title}
                    </button>
                  </li>
                ))}
            </ul>
          </div>

          <div className="tab-content mt-4 h-full px-5 w-full">
            {data && activeTab === TEXT_CONSTANTS.Decription && (
              <div
                className="p-4 bg-gray-50 rounded-lg dark:bg-gray-800"
                role="tabpanel"
              >
                <img
                  className="h-[700px] w-[700px] mx-auto"
                  src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2021/08/20215108/1200.png"
                />
                <HtmlContent
                  className=""
                  html={data.product.detailDecription}
                />
              </div>
            )}

            {activeTab === TEXT_CONSTANTS.Rating && (
              <div
                className="p-4 bg-gray-50 rounded-lg dark:bg-gray-800"
                role="tabpanel"
              >
                <p className="text-sm text-gray-500 dark:text-gray-400">
                  This is some placeholder content for the
                  <strong className="font-medium text-gray-800 dark:text-white">
                    Dashboard
                  </strong>
                  tab.
                </p>
              </div>
            )}
            {activeTab === TEXT_CONSTANTS.BuyGuide && <BuyGuide />}
            {activeTab === TEXT_CONSTANTS.RefundPolicy && <RefundPolicy />}
            {activeTab === TEXT_CONSTANTS.SizeGuide && <SizeGuide />}
            {brach && activeTab === TEXT_CONSTANTS.ShowRoom && (
              <div className="p-4 bg-gray-50 rounded-lg dark:bg-gray-800 list-none ">
                <img src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2021/08/20215108/1200.png" />
                {convertBranchObjectToArray.map((item,index) => {
                  return (
                    <div key={index}>
                      <li className=" w-full border-t-4">
                        <p className="text-lg border-b-2 w-full truncate uppercase font-semibold">
                          {"Show Room Elly " + item.region}
                        </p>
                      </li>
                      {item.data.map((branch) => {
                        return (
                          <li className="w-full py-1 border-b" key={branch.id}>
                            <p className="text-base w-full font-medium inline">
                              {branch.branchName}:
                            </p>
                            <p className="inline"> {branch.address}</p>
                          </li>
                        );
                      })}
                    </div>
                  );
                })}
              </div>
            )}
          </div>

          {/* <div className="">
            <div className="grid grid-cols-12">
              <div className="flex col-span-6 text-center bg-red-600">
                <a className="devvn_buy_now devvn_buy_now_style">
                  <strong>Đặt hàng nhanh</strong>
                  <span>Giao hàng nhanh toàn quốc miễn phí</span>
                </a>
              </div>
              <div className="col-span-6">
                <ul>
                  <li>
                    <i className="fa fa-truck" /> Giao hàng nhanh miễn phí tận
                    nơi
                  </li>
                  <li>
                    <i className="fa fa-user-shield" /> Bảo mật thông tin tuyệt
                    đối
                  </li>
                  <li>
                    <i className="fa fa-user-check" /> Kiểm tra hàng mới thanh
                    toán
                  </li>
                  <li>
                    <i className="fa fa-retweet" /> Bảo dưỡng sản phẩm trọn đời
                  </li>
                </ul>
              </div>
            </div>
            <ul className="row row2 flex">
              <li li className=""className="col-span-4">
                <a href="tel:0966353000">
                  <span className="fa-stack" aria-hidden="true">
                    <i className="fa fa-circle fa-stack-2x" />
                    <i className="fa fa-phone fa-stack-1x fa-inverse" />
                  </span>
                  Gọi đặt mua: <span>0966.353.000</span>
                </a>
              </li>
              <li li className=""className="col-span-4">
                <a
                  href="https://m.me/ThuongHieuThoiTrangELLY"
                  target="_blank"
                  rel="nofollow"
                >
                  <span className="fa-stack" aria-hidden="true">
                    <i className="fa fa-circle fa-stack-2x" />
                    <i className="fa fa-comments fa-stack-1x fa-inverse" />
                  </span>
                  <span>Tư vấn online</span>
                </a>
              </li>
              <li li className=""className="col-span-4">
                <a href="/he-thong-showroom">
                  <span className="fa-stack" aria-hidden="true">
                    <i className="fa fa-circle fa-stack-2x" />
                    <i className="fa fa-home fa-stack-1x fa-inverse" />
                  </span>
                  Hệ thống <span>Showroom ELLY</span>
                </a>
              </li>
            </ul>
          </div> */}
        </div>
      </div>
    </div>
  );
};

export default MultiTaps;
