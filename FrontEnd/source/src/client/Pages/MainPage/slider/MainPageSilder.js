import React from "react";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const images = [
  {
    src: "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/08/15134745/1970x780-1.jpg",
    alt: "Image 1",
  },
  {
    src: "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/08/17001317/1970x780-2.jpg",
    alt: "Image 2",
  },
  {
    src: "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/05/28134855/1970x780-1.jpg",
    alt: "Image 3",
  },
  {
    src: "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/07/31090528/1970x780-1.jpg",
    alt: "Image 4",
  },
  {
    src: "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2023/07/25125356/z4545479213241_783f7f2d1301a3bfb953a1225a0e1960.jpg",
    alt: "Image 5",
  },
  {
    src: "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/01/23123856/1920x780-HA-VER-01.jpg",
    alt: "Image 6",
  },
  // Add more images as needed
];

const MainPageSilder = () => {
  return (
    <div id="default-carousel" className="relative w-full" data-carousel="slide">
    {/* Carousel wrapper */}
    <div className="relative h-[900px] overflow-hidden rounded-lg md:h-[800px]">
      {/* Item 1 */}
      <div className="hidden duration-700 ease-in-out" data-carousel-item="">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/08/15134745/1970x780-1.jpg"
          className="absolute block w-full -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2"
          alt="..."
        />
      </div>
      {/* Item 2 */}
      <div className="hidden duration-700 ease-in-out" data-carousel-item="">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/08/17001317/1970x780-2.jpg"
          className="absolute block w-full -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2"
          alt="..."
        />
      </div>
      {/* Item 3 */}
      <div className="hidden duration-700 ease-in-out" data-carousel-item="">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/05/28134855/1970x780-1.jpg"
          className="absolute block w-full -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2"
          alt="..."
        />
      </div>
      {/* Item 4 */}
      <div className="hidden duration-700 ease-in-out" data-carousel-item="">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/07/31090528/1970x780-1.jpg"
          className="absolute block w-full -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2"
          alt="..."
        />
      </div>
      {/* Item 5 */}
      <div className="hidden duration-700 ease-in-out" data-carousel-item="">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2023/07/25125356/z4545479213241_783f7f2d1301a3bfb953a1225a0e1960.jpg"
          className="absolute block w-full -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2"
          alt="..."
        />
      </div>
      {/* Item 6 */}
      <div className="hidden duration-700 ease-in-out" data-carousel-item="">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/01/23123856/1920x780-HA-VER-01.jpg"
          className="absolute block w-full -translate-x-1/2 -translate-y-1/2 top-1/2 left-1/2"
          alt="..."
        />
      </div>
    </div>
    {/* Slider indicators */}
    <div className="absolute z-30 flex -translate-x-1/2 bottom-5 left-1/2 space-x-3 rtl:space-x-reverse">
      <button
        type="button"
        className="w-3 h-3 rounded-full"
        aria-current="true"
        aria-label="Slide 1"
        data-carousel-slide-to={0}
      />
      <button
        type="button"
        className="w-3 h-3 rounded-full"
        aria-current="false"
        aria-label="Slide 2"
        data-carousel-slide-to={1}
      />
      <button
        type="button"
        className="w-3 h-3 rounded-full"
        aria-current="false"
        aria-label="Slide 3"
        data-carousel-slide-to={2}
      />
      <button
        type="button"
        className="w-3 h-3 rounded-full"
        aria-current="false"
        aria-label="Slide 4"
        data-carousel-slide-to={3}
      />
      <button
        type="button"
        className="w-3 h-3 rounded-full"
        aria-current="false"
        aria-label="Slide 5"
        data-carousel-slide-to={4}
      />
        <button
        type="button"
        className="w-3 h-3 rounded-full"
        aria-current="false"
        aria-label="Slide 6"
        data-carousel-slide-to={5}
      />
    </div>
    {/* Slider controls */}
    <button
      type="button"
      className="absolute top-0 start-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
      data-carousel-prev=""
    >
      <span className="inline-flex items-center justify-center w-10 h-10 rounded-full bg-white/30 dark:bg-gray-800/30 group-hover:bg-white/50 dark:group-hover:bg-gray-800/60 group-focus:ring-4 group-focus:ring-white dark:group-focus:ring-gray-800/70 group-focus:outline-none">
        <svg
          className="w-4 h-4 text-white dark:text-gray-800 rtl:rotate-180"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 6 10"
        >
          <path
            stroke="currentColor"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M5 1 1 5l4 4"
          />
        </svg>
        <span className="sr-only">Previous</span>
      </span>
    </button>
    <button
      type="button"
      className="absolute top-0 end-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
      data-carousel-next=""
    >
      <span className="inline-flex items-center justify-center w-10 h-10 rounded-full bg-white/30 dark:bg-gray-800/30 group-hover:bg-white/50 dark:group-hover:bg-gray-800/60 group-focus:ring-4 group-focus:ring-white dark:group-focus:ring-gray-800/70 group-focus:outline-none">
        <svg
          className="w-4 h-4 text-white dark:text-gray-800 rtl:rotate-180"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 6 10"
        >
          <path
            stroke="currentColor"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="m1 9 4-4-4-4"
          />
        </svg>
        <span className="sr-only">Next</span>
      </span>
    </button>
  </div>
  );
}

export default MainPageSilder;
