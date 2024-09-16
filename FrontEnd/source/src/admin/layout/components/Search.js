import React from "react";

const Search = () => {
  return (
    <div className="hidden sm:block">
      <form action="https://formbold.com/s/unique_form_id" method="POST">
        <div className="relative">
          <button className="absolute left-0 top-1/2 -translate-y-1/2">
            <i className="fa-solid fa-magnifying-glass"></i>
          </button>
          <input
            placeholder="Type to search..."
            className="w-full bg-transparent pl-9 pr-4 xl:w-125 border-none focus:outline-none focus:border-transparent focus:outline-0"
          />
        </div>
      </form>
    </div>
  );
};

export default Search;
