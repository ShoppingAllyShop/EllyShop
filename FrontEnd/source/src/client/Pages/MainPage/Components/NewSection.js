import React from "react";
import NewsCard from "./NewsCard";
import ShowRoom from "./ShowRoom";

const NewSection = ({data}) => {
  console.log("221312",data)
  const { branchList,newsList,prizeList } = data
  return (
    <section className="pl-[100px] pr-[100px]">
      <div className="grid grid-cols-3 gap-4 w-full">
       {prizeList && <NewsCard data={prizeList} title={"Danh hiệu và giải thưởng"}/>} 
       {branchList && <ShowRoom data={branchList} title={"Hệ Thống ShowRoom"}/>} 
       {newsList && <NewsCard data={newsList} title={"Tin Tức"}/>} 
      </div>
    </section>
  );
};

export default NewSection;
