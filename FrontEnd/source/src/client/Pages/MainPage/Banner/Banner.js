import React from 'react'

const Banner = () => {
  return (
    <section 
    className="relative bg-cover bg-center h-[310px] bg-[url('https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2021/02/06142841/mlhtrend-1024x184.jpg')] "
>
    <div className="absolute inset-0 bg-black opacity-50"></div>
    <div className="relative z-10 flex items-center justify-center h-full px-6 text-center text-white">
        <div>
            <h3 className="text-4xl md:text-3xl font-bold mb-4">
            ELLY – LEADING THE TREND
            </h3>
            <p className="text-lg md:text-2xl mb-8">
            ELLY là sự kết hợp hoàn hảo giữa phong cách thời trang Pháp quyến rũ, sang trọng, hiện đại và phong cách thời trang Á Đông thanh lịch.
            </p>
            <a 
                href="#"
                className="uppercase bg-black hover:bg-white hover:text-gray-600 text-white font-bold py-3 px-6 rounded-lg transition duration-300 text-xl border-white border-2"
            >
                About Us
            </a>
        </div>
    </div>
</section>
  )
}

export default Banner