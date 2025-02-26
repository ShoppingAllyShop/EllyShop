import React, { useState } from 'react'

const ZoomImage = ({src, zoomLevel=150}) => {
    const [backgroundState, setBackgroundState] = useState({});
    const handleMouseMove = (e) => {
    const { left, top, width, height } = e.target.getBoundingClientRect();

      const mouseY = e.clientY; // Vị trí chuột theo trục Y so với viewport(mép trên của browser)
      const mouseRelativeY = mouseY - top; // Vị trí chuột so với phần tử
      const distanceToTop = top + mouseRelativeY; // Khoảng cách từ chuột đến cạnh trên của viewport (mép trên của browser)
      const x = ((e.pageX - left) / width) * 100;
      const y = ((distanceToTop - top) / height) * 100;
      setBackgroundState({
        backgroundImage: `url(${src})`,
        backgroundPosition: `${x}% ${y}%`,
        backgroundSize: `${zoomLevel}%`
      });
    };
    return (
        <figure
          onMouseMove={handleMouseMove}
          style={backgroundState}
          className="group bg-no-repeat cursor-pointer"
        >
          <img className='group-hover:opacity-0 opacity-1 w-full ' src={src} />
        </figure>
      );
}

export default ZoomImage