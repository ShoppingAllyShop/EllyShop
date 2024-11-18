import React from "react";
import { Link } from "react-router-dom";
import { BUTTON_TYPE } from "../../../../constants/common";

const BottomPaging = ({paging, onSearchEmployeeUser}) => {

    const handleSetSearchParams = (e, type) => {
        let params = {
            PageNumber: paging?.pageNumber,
            PageSize: paging?.pageSize,
            SortBy: paging?.sortBy,
            SortOrder: paging?.sortOrder,
        };
        switch (type) {
            case BUTTON_TYPE.NEXT:
                if(!paging.hasNextPage) return
                params.PageNumber = paging.pageNumber+1
                break;
            case BUTTON_TYPE.PREV:
                if(!paging.hasPreviousPage) return                
                params.PageNumber = paging.pageNumber-1
                break;
            case BUTTON_TYPE.NUMBER:
                params.PageNumber = e.target.innerText
                break;
            default:
                break;
        }

        //call back parent
        onSearchEmployeeUser(params)
    };

    const renderPrevButton = () => {
      return (
        <li className={`datatable-pagination-list-item datatable-hidden ${paging?.hasNextPage ? "" : 'datatable-disabled'}`}
          onClick={(e) => handleSetSearchParams(e, BUTTON_TYPE.PREV)}
        >
          <Link className={`flex items-center justify-center h-full py-1.5 px-3 ml-0 rounded-l-lg border border-gray-300 hover:bg-gray-300 hover:text-gray-700
                    ${paging?.hasPreviousPage ? "bg-white text-gray-500" : 'bg-gray-300 border-gray-300 text-gray-700'}`}
          >
            <span className="sr-only">Previous</span>
            <i className="fa-solid fa-angle-left"></i>
          </Link>
        </li>
      );
    };    

    const renderNextButton = () => {
      return (
        <li className={`datatable-pagination-list-item datatable-hidden ` }
          onClick={(e) => handleSetSearchParams(e, BUTTON_TYPE.NEXT)}
        >
          <Link className={`flex items-center justify-center h-full py-1.5 px-3 leading-tight rounded-r-lg border border-gray-300 hover:bg-gray-300 hover:text-gray-700
                          ${paging?.hasNextPage ? "bg-white text-gray-500" : 'bg-gray-300 border-gray-300 text-gray-700'}`}
          >
            <span className="sr-only">Next</span>
            <i className="fa-solid fa-angle-right"></i>
          </Link>
        </li>
      );
    };
    
    const renderSelectionPagingNumber = () => {
      return (
        <ul className="inline-flex items-stretch -space-x-px">
          {renderPrevButton()}
          <li onClick={(e) => handleSetSearchParams(e, BUTTON_TYPE.NUMBER)}>
            <Link className="flex items-center justify-center text-sm py-2 px-3 leading-tight border
                          border-gray-300 dark:bg-gray-800 dark:border-gray-700 dark:hover:bg-gray-700
                           dark:hover:text-white font-semibold text-gray-900 dark:text-white"
            >
              {`Trang ${paging?.pageNumber}/${paging?.totalPages}`}
            </Link>
          </li>
          {renderNextButton()}
        </ul>
      );
    };    

  return (
    <nav className="flex flex-col md:flex-row justify-between items-start md:items-center space-y-3 md:space-y-0 p-4 place-self-center"
      aria-label="Table navigation "
    >
      {renderSelectionPagingNumber()}
    </nav>
  );
};

export default BottomPaging;
