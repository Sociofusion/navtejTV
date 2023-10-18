import React, { Fragment, useEffect } from "react";
import Axios from "axios";
import configData from "./Config";
import { Helmet } from "react-helmet";

export default function TopHeader(props) {
  const [settingData, setSettingData] = React.useState({
    FacebookLink: "",
    TwitterLink: "",
    InstagramLink: "",
    YoutubeLink: "",
    LogoLiveUrl: "",
    FooterLogoLiveUrl: "",
    YoutubeVideoURL: "",
    MetaTags: "",
    GoogleAnalytics: "",
    Address: "",
    MailID: "",
    Mobile1: "",
    Mobile2: "",
    Copyright: "",
  });

  const [languages, setLanguages] = React.useState([]);
  const [lang, setLang] = React.useState(localStorage.getItem("site_lang") ? localStorage.getItem("site_lang") : 3);

  const axiosConfig = {
    headers: {
      sessionToken: configData.SESSION_TOKEN,
    },
  };

  const site_lang = localStorage.getItem("site_lang") ? localStorage.getItem("site_lang") : 3;

  useEffect(() => {

    Axios.get(configData.SETTING_URL, axiosConfig).then((response) => {
      setSettingData(JSON.parse(response.data.payload)[0]);
    });

    Axios.get(configData.GET_LANGUAGES, axiosConfig).then((response) => {
      setLanguages(JSON.parse(response.data.payload));
      const success = response.data.success;
      if (success) {
        document.getElementById("languageChange").value = site_lang;
      }
    });


  }, []);

  function setLangv() {
    const langv = document.getElementById("languageChange").value;
    setLang(langv)
    window.location.reload();
  }

  useEffect(() => {
    localStorage.setItem("site_lang", lang);
  }, [lang]);

  return settingData ? (
    <>
      <Helmet>
        {/* <!-- SEO Starts --> */}
        <meta name="description" content={settingData.MetaTags} />
        <meta name="keywords" content={settingData.MetaTags} />
        <meta property="og:title" content="Navtej Tv" />
        <meta property="og:type" content="website" />
        <meta property="og:url" content="https://navtejtv.com/" />
        <meta property="og:description" content={settingData.MetaTags} />
        <meta property="og:site_name" content="Navtej Tv" />
        <link rel="canonical" href="https://navtejtv.com/" />
        {/* <!-- SEO END --> */}
      </Helmet>
      <Fragment>
        <section className="main-top-header">
          <div className="container">
            <div className="row">
              <div className="align-self-center">
                <div className="top-header-content">
                  <div className="col-xl-2 col-lg-3 col-md-3 col-sm-6 col-xs-12 hideMobile">
                    <div className="navbar-logo">
                      <a className="navbar-brand" href="/">
                        <img src={settingData.LogoLiveUrl} alt="" />
                      </a>
                    </div>
                  </div>

                  <div className="col-xl-5 col-lg-5 col-md-5 col-sm-6 col-xs-6">
                    <div className="left-content">
                      <ul className="list">
                       
                        <li>
                          <a href="javascript:void(0)">
                            <i className="fa fa-calendar-alt"></i> &nbsp;
                            {props.todayDate}
                          </a>
                        </li>
                      </ul>
                    </div>
                  </div>

                  <div className="col-xl-5 col-lg-5 col-md-5 col-sm-6 col-xs-6">
                    <div className="left-content">
                      <ul className="list">
                        <a href="#">
                          <i className="fa fa-globe text-white"></i>
                        </a>
                        <li className="border_right">




                          <select id="languageChange" onChange={setLangv}>
                            {languages && languages.length > 0
                              ? languages.map((l, index) => {
                                return <option selected = { localStorage.getItem("site_lang") == l.ID ? true : false} value={l.ID}>{l.Name}</option>;
                              })
                              : ""}
                          </select>
                        </li>

                        <li>
                          <a
                            href={settingData.FacebookLink}
                            target="_blank"
                            className="facebook"
                          >
                            <i className="fab fa-facebook-f"></i>
                          </a>
                        </li>

                        <li>
                          <a
                            href={settingData.TwitterLink}
                            target="_blank"
                            className="twitter"
                          >
                            <i className="fab fa-twitter"></i>
                          </a>
                        </li>

                        <li>
                          <a
                            href={settingData.InstagramLink}
                            target="_blank"
                            className="instagram"
                          >
                            <i className="fab fa-instagram"></i>
                          </a>
                        </li>

                        <li>
                          <a
                            href={settingData.YoutubeLink}
                            target="_blank"
                            className="youtube"
                          >
                            <i className="fab fa-youtube"></i>
                          </a>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </Fragment>{" "}
    </>
  ) : (
    ""
  );
}
