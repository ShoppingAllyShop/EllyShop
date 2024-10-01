import React from "react";
import NewsCard from "./NewsCard";
import ShowRoom from "./ShowRoom";

const NewSection = ({data}) => {
  const { branchList,newList,prizeList } = data
  console.log("newSectiondata",data)
  // const newsData = data.news.filter(
  //   (item) =>
  //   item.type === "News"
  // );
  // const prizeData = data.news.filter(
  //   (item) =>
  //   item.type === "Prize" 
  // );
  return (
    <section className="pl-[100px] pr-[100px]">
      <div className="grid grid-cols-3 gap-4 w-full">
      {prizeList && <NewsCard data={prizeList}/>} 
       {branchList && <ShowRoom data={branchList}/>} 
       {newList && <NewsCard data={newList}/>} 
      </div>
    </section>
  );
};

export default NewSection;
