import React from "react";

const ShowRoom = ({data}) => {
  const branchs = data.branch;
  console.log("branchs".branchs)
  const groupedByRegion = branchs.reduce((acc, item) => {
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
    <div className="ShowRoom">
      <div className="">
        <p className="uppercase font-bold text-center border box-border p-4"> Hệ Thống ShowRoom </p>
      </div>
      <div className="menu p-2">
      <ul className="p-2 scrollbar-custom overflow-y-scroll h-[52rem]">
          {convertBranchObjectToArray.map((item) => {
            return (
              <>
                <li className="h-[32px] w-full">
                  <p className="text-lg border-b-2 w-full truncate uppercase font-semibold">
                    {"Show Room Elly " + item.region}
                  </p>
                </li>
                {item.branchs.map((branch) => {
                  return (
                    <li className="w-full py-1 border-b">
                      <p className="text-base w-full font-medium inline">
                        {branch.branchName}:
                      </p>
                      <p className="inline"> {branch.address}</p>
                    </li>
                  );
                })}
              </>
            );
          })}
        </ul>
      </div>
    </div>
  );
};

export default ShowRoom;
