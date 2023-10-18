import React, { Fragment, useEffect } from "react";
import configData from "./Config";



export default function MainMenuSubCategory(props) {
  function showMenuTab(e){
    const element = document.getElementById(e);
    // const allElements = document.getElementsByClassName('go-tab-c');
    const nodeList = document.querySelectorAll(".go-tab-c");
    for (let i = 0; i < nodeList.length; i++) {
      nodeList[i].classList.remove('active');
    }

    element.classList.add("active");
  }
  return (
        <a className="nav-link tab-link" 
        onMouseOver={()=>{showMenuTab(props.dataTab)}}  
        href={props.GotoStatePage ?`/state/${props.categorySlug}`:`${configData.BASE_URL_CATEGORY}${props.categorySlug}`} 
        data-tab={`#${props.dataTab}`}>
        {props.categoryName}
        </a>
  );
}
