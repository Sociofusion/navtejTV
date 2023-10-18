import React, { Fragment, useEffect, useState } from 'react'
import configData from "./Config";
import Axios from "axios"
import MainMenuCategory from './MainMenuCategory';

export default function MainMenu(props) {

    const menus = props.menus;
    const settingData = props.settingData;
    const site_lang = props.site_lang;
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
        <div className="mainmenu-area">
            <div className="container">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="navsm" style={{ alignItems: "center" }}>
                            <div className="toogle-icon">
                                <i className="fas fa-bars"></i>
                            </div>
                            <a className="navbar-brand" href="/"> <img src={SettingData.LogoLiveUrl} alt="" /></a>
                            <div className="serch-icon-area" onClick={() => setSearchField(!searchField)}>
                                <p className="web-serch">
                                    <i className="fas fa-search"></i>
                                </p>
                            </div>
                            {searchField == true &&
                                <div className="search-form-area">
                                    <form className="search-form" action="/news-search/" method="get">
                                        <input type="text" name="search" placeholder={site_lang == 3 ? "आप जो चाहते हैं उसे खोजें" : "Search what you want"} />
                                         
                                    </form>
                                </div>
                            }
                        </div>
                        <nav className="navbar navbar-expand-lg navbar-light menulg">
                            <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#main_menu"
                                aria-controls="main_menu" aria-expanded="false" aria-label="Toggle navigation">
                                <span className="navbar-toggler-icon"></span>
                            </button>

                            <div className="collapse navbar-collapse fixed-height" id="main_menu">
                                <ul className="navbar-nav mr-auto">
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
                            <div className="serch-icon-area" onClick={() => setSearchField(!searchField)}>
                                <p className="web-serch">
                                    <i className="fas fa-search"></i>
                                </p>
                            </div>
                            {searchField == true &&
                                <div className="search-form-area">
                                    <form className="search-form" action="/news-search/" method="get">
                                        <input type="text" name="search" placeholder="आप जो चाहते हैं उसे खोजें" />
                                    </form>
                                </div>
                            }
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    )

}