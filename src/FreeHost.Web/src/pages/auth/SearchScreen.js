import React from 'react'

import Search from '../../components/Search'
import "../../css/pages/SearchScreen.css"

function SearchScreen() {
  return (
    <div className='searchSc-page'>
      <div className='searchSc-container'>
        <h1 className='searchSc-header'>Find Apartments</h1>
        <Search/>
      </div>
    </div>
  )
}

export default SearchScreen