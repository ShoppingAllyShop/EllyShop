import React from "react";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {faChevronRight} from "@fortawesome/free-solid-svg-icons";
const ShowRoom = ({ data, title }) => {  
  const groupedByRegion = data.reduce((acc, item) => {
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
      branchs: value,
    })
  );
  return (
    <div className="ShowRoom h-[730px]">
      <p className="uppercase text-center text-2xl first-line:font-bold border py-2">
        {title}
      </p>
      <div className=" px-4 flex flex-col border box-border">
        <div className="h-[375px] truncate">
          <img
            className=""
            src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2022/02/22003913/sr5-1-1-e1669436859285.jpg"
          />
        </div>
        <ul className="p-2 scrollbar-custom overflow-y-scroll h-[12rem]">
          {convertBranchObjectToArray.map((item,index) => {
            return (
              <div key={index}>
                <li className="h-[32px] w-full">
                  <p className="text-lg border-b-2 w-full truncate uppercase font-semibold">
                    {"Show Room Elly " + item.region}
                  </p>
                </li>
                {item.branchs.map((branchs) => {
                  return (
                    <li className="w-full py-1 border-b">
                      <p className="text-base w-full font-medium inline">
                        {branchs.branchName}:
                      </p>
                      <p className="inline"> {branchs.address}</p>
                    </li>
                  );
                })}
              </div>
            );
          })}
        </ul>
        <div className="text-center pb-7 pt-2">
          <Link to={"https://www.google.com/"} className="underline ">
            Xem Tất Cả <FontAwesomeIcon className="px-2 text-1xl" icon={faChevronRight} />
          </Link>
        </div>
      </div>
    </div>
  );
};

export default ShowRoom;
