import React from "react";

const ShowRoom = ({ data }) => {

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
      data: value,
    })
  );
  return (
    <div className="ShowRoom">
      <div>
        <p className="uppercase font-bold text-center border box-border p-4">
          Hệ Thống ShowRoom
        </p>
      </div>
      <div className="menu p-2">
        <ul className="p-2 scrollbar-custom overflow-y-scroll h-[52rem]">
          {convertBranchObjectToArray.map((item,index) => {
            return (
              <div key={index}>
                <li className="h-[32px] w-full">
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
        </ul>
      </div>
    </div>
  );
};

export default ShowRoom;
