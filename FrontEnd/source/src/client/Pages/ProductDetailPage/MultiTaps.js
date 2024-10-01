import React, { useState } from "react";
import HtmlContent from "../../../components/HtmlContent";

const MultiTaps = (props) => {
  const [activeTab, setActiveTab] = useState(0);
  const multiTapdata = props.data.decription;
  console.log("multiTapdata",multiTapdata)

  return (
    <div className="MultiTaps ">
      <div className="container ">
        <div className="tabs border-2">
          <div className="tab-list flex border box-border">
            {multiTapdata.map((item, index) => (
              <button
                key={index}
                className={`flex-1 py-2 px-4 text-center uppercase bg-gray-100 border-2 ${
                  activeTab === index
                    ? "border-t-2 border-t-red-500 bg-white"
                    : "border-transparent"
                } hover:border-t-2 `}
                onClick={() => setActiveTab(index)}
              >
                {item.title}
              </button>
            ))}
          </div>
          <div className="tab-content mt-4 h-full px-5 w-full">
            <div className="tab-pane h-full w-[500px] ">
              <HtmlContent html={multiTapdata[activeTab].detailDescription} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MultiTaps;
