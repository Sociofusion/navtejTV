import React, { Fragment, useEffect } from "react";
import MainMenuSubCategory from "./MainMenuSubCategory";
import MainMenuSubCategoryTab from "./MainMenuSubCategoryTab";
import configData from "./Config";


function CategoryWithoutChild(props) {

  return (
    <Fragment>
      <li className="nav-item">
        <a
          className="nav-link active"
          //href={`${configData.BASE_URL_CATEGORY}${props.categorySlug}`}
          href={props.GotoStatePage ? `/state/${props.categorySlug}` : `${configData.BASE_URL_CATEGORY}${props.categorySlug}`}
        >
          {props.categoryName}
        </a>
      </li>
    </Fragment>
  );
}

function CategoryWithChild(props) {
  const childMenu = props.childMenu;
  return (
    <Fragment>
      <li className="nav-item dropdown mega-menu">
        <a className="nav-link dropdown-toggle"
          //href={`${configData.BASE_URL_CATEGORY}${props.categorySlug}`}
          href={props.GotoStatePage ? `/state/${props.categorySlug}` : `${configData.BASE_URL_CATEGORY}${props.categorySlug}`}
        >
          {props.categoryName}
        </a>
        <div className="dropdown-menu">
          <div className="row m-0 p-0">
            <div className="col-lg-2 p-0">
              <div className="nav flex-column">
                {childMenu.map((cMenu) => {
                  return (
                    <MainMenuSubCategory
                      categoryUrl={`${configData.BASE_URL_CATEGORY}${cMenu.ID}/${cMenu.MenuTitle}`}
                      categoryName={cMenu.MenuTitle}
                      dataTab={`${cMenu.ID}`}
                      categorySlug={cMenu.Slug}
                      GotoStatePage={cMenu.GotoStatePage}
                    />
                  );
                })}
              </div>
            </div>
            <div className="col-lg-10">
              <div className="tab-content">
                {childMenu.map((cMenu, i) => {
                  return <MainMenuSubCategoryTab index={i} id={cMenu.ID}
                    categoryName={cMenu.MenuTitle}
                    categorySlug={cMenu.Slug} />;
                })}
              </div>
            </div>
          </div>
        </div>
      </li>
    </Fragment>
  );
}

export default function MainMenuCategory(props) {
  const isChild = props.isChild;

  if (isChild) {
    return (
      <CategoryWithChild
        categoryId={props.categoryId}
        categoryName={props.categoryName}
        categorySlug={props.categorySlug}
        childMenu={props.childMenu}
        GotoStatePage={props.GotoStatePage}
      />
    );
  } else {
    return (
      <CategoryWithoutChild
        categoryId={props.categoryId}
        categoryName={props.categoryName}
        categorySlug={props.categorySlug}
        GotoStatePage={props.GotoStatePage}
      />
    );
  }
}
