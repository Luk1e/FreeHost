import React,{useState} from 'react';

import { useNavigate, useSearchParams } from "react-router-dom";

import "../css/components/Search.css";
function Search() {
  return (
<div className='search-container'>
  <input type="text" className='search-input'/>
  <input type="text"  className='search-input'/>
  <button className='search-btn'>Search</button>
</div>  )
}

export default Search