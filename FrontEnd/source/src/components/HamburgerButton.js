import React from 'react'

const HamburgerButton = ({size, handleClick}) => {
    let itemStyle = 'bg-black block w-full h-[2px] rounded transition-all origin-[1px] group-hover:bg-gray-400'
    //let itemStyle = 'bg-black block w-8 h-[0.35rem] rounded transition-all origin-[1px]'
    return (
        <button onClick={handleClick} type="button" className={`size-${size} flex justify-around 
        flex-col flex-wrap z-10 cursor-pointer group p-1`}>
            <div className={`${itemStyle}`}/>
            <div className={`${itemStyle}`}/>
            <div className={`${itemStyle}`}/>
        </button>
    );
}

export default HamburgerButton
