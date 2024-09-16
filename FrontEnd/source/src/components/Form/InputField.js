import React from 'react'

const InputField = ({ label, register, name, errors, ...rest }) => (
  <div>
    <label className='float-left'>{label}</label>
    {errors[name] && <p className='float-end text-red-600 italic font-medium text-sm'>{errors[name].message}</p>}
    <input {...register(name)} {...rest} />   
  </div>
);

export default InputField
