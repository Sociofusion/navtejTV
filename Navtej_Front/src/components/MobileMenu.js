import React, { Fragment, useEffect, useState } from 'react'
import configData from "./Config";
import Axios from "axios";
import MainMenuCategory from './MainMenuCategory';

export default function MobileMenu(props) {

    const menus = props.menus;
    const settingData = props.settingData;
    const [searchField, setSearchField] = useState(false);
    const [SettingData, setSettingData] = React.useState({
        LogoLiveUrl: "",
    })
    const axiosConfig = {
        headers: {
            sessionToken: configData.SESSION_TOKEN,
        },
    };

    // Get Setting
    useEffect(() => {
        Axios.get(configData.SETTING_URL, axiosConfig).then((response) => {
            setSettingData(JSON.parse(response.data.payload)[0]);
        });
    }, [])


    return (
        <div className="mobile-menu">
            <div className="logo-area">
                <a className="navbar-brand" href="/">
                    <img src={SettingData.FooterLogoLiveUrl} alt="" />
                </a>
                <div className="close-menu">
                    <i className="fas fa-times"></i>
                </div>
            </div>
            <ul className="mobile-menu-list">
                {menus.map(menu => {
                    return (
                        <MainMenuCategory categorySlug={menu.Slug} categoryId={menu.ID}
                            GotoStatePage={menu.GotoStatePage}
                            categoryName={menu.MenuTitle}
                            isChild={menu.childs.length > 0 ? true : false}
                            childMenu={menu.childs.length > 0 ? menu.childs : false} />
                    )
                })}

            </ul>
        </div>
    )

}