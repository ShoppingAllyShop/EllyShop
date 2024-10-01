import React, { useState } from "react";

const ZoomImage = (props) => {
  const { src } = props;
  const [figureStyle, setFigureStyle] = useState({
    backgroundImage: `url(${src})`,
    backgroundPosition: '0% 0%'
  });

  const handleMouseMove = (e) => {
    const { left, top, width, height } = e.target.getBoundingClientRect();
    const x = ((e.pageX - left) / width) * 100;
    const y = ((e.pageY - top) / height) * 100;
    setFigureStyle({ backgroundImage: `url(${src})`, backgroundPosition: `${x}% ${y}%` });
  };

  return (
    <figure
      onMouseMove={handleMouseMove}
      style={figureStyle}
      className="group bg-no-repeat"
    >
      <img
        className="group-hover:opacity-0 block"
        alt={`Main Image`}
        src={src}
      />
    </figure>
  );
};

export default ZoomImage;
