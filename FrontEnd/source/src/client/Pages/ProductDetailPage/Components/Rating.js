import React, { useState } from "react";

const Rating = ({ totalStars = 5 }) => {
  const [rating, setRating] = useState(0); // Store the selected rating

  const handleRating = (starValue) => {
    setRating(starValue); // Set the clicked star as the rating
  };

  return (
    <div>
      {[...Array(totalStars)].map((_, index) => {
        const starValue = index + 1;
        return (
          <span
            key={index}
            onClick={() => handleRating(starValue)}
            style={{ cursor: "pointer", color: starValue <= rating ? "gold" : "gray" }}
          >
            ★
          </span>
        );
      })}
    </div>
  );
};

export default Rating;