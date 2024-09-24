import React from 'react'

const InputField = ({ label, labelStyle, register, name, errors, placeHolder, ...rest }) => (
  <div>
    <label className={`float-left ${labelStyle}`}>{label}</label> 
    {errors[name] && <p className='float-end text-red-600 italic font-medium text-sm'>{errors[name].message}</p>}
    <input {...register(name)} {...rest} placeholder={placeHolder}/>   
  </div>
);

export default InputField
