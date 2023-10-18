import React, { useEffect } from "react";
import logo from "./logo.svg";
import { ReactDOM } from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./components/Home";
import CategoryPage from "./components/CategoryPage";
import CategoryDetailPage from "./components/CategoryDetailPage";
import Axios from "axios";
import configData from "./components/Config";
import About from "./components/About";
import Advertise from "./components/Advertise";
import Support from "./components/Support";
import Features from "./components/Features";
import State from "./components/state/State";
import ContactUs from "./components/Contact/ContactUs";
import Privacy_Policy from "./components/Privacy_Policy";


function App() {
  const [menus, setMenus] = React.useState([]);
  const [marqueeNews, setMarqueeNews] = React.useState([]);
  const [adHeader, setAdHeader] = React.useState();
  const [adFooter, setAdFooter] = React.useState();
  const [adRight, setAdRight] = React.useState();
  const [adRight2, setAdRight2] = React.useState();
  const [adRight3, setAdRight3] = React.useState();
  const [homeSlider, setHomeSlider] = React.useState([]);
  const [footerMenu, setFooterMenu] = React.useState([]);
  const [homeRightSlider, sethomeRightSlider] = React.useState([]);
  const [post, setPost] = React.useState([]);
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
  })

  const headerMenuApiUrl = configData.MENU_API_URL;
  const marqueeApiUrl = configData.MARQUEE_API_URL;
  const adHeaderApiUrl = configData.AD_API_URL + "home-header1";
  const adFooterApiUrl = configData.AD_API_URL + "home-footer1";
  const adRightApiUrl = configData.AD_API_URL + "home-right1";
  const adRight2ApiUrl = configData.AD_API_URL + "home-right2";
  const adRight3ApiUrl = configData.AD_API_URL + "home-right3";
  const homeSliderApiUrl = configData.HOME_SLIDER_API_URL;
  const footerMenuApiUrl = configData.FOOTER_MENU_API_URL;
  const HOME_Right_SLIDER_API_URL = configData.HOME_Right_SLIDER_API_URL;
  const monthNames = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
  ];
  let newDate = new Date();
  const todayDate = newDate.getDate() + " " + monthNames[newDate.getMonth()] + " " + newDate.getFullYear();
  const site_lang = localStorage.getItem("site_lang") ? localStorage.getItem("site_lang") : 3;

  const axiosConfig = {
    headers: {
      sessionToken: configData.SESSION_TOKEN,
    },
  };

  useEffect(() => {
    // Getting Top Header Menus
    Axios.get(headerMenuApiUrl, axiosConfig).then((response) => {
      setMenus(JSON.parse(response.data.payload));
    });

    // marqueeMenus
    Axios.get(marqueeApiUrl, axiosConfig).then((response) => {
      setMarqueeNews(
        response.data.payload == null ? [] : JSON.parse(response.data.payload)
      );
    });

    // Ads Header
    Axios.get(adHeaderApiUrl, axiosConfig).then((response) => {
      setAdHeader(JSON.parse(response.data.payload));
    });

    // Ads Footer
    Axios.get(adFooterApiUrl, axiosConfig).then((response) => {
      setAdFooter(JSON.parse(response.data.payload));
    });

    // Ads Right
    Axios.get(adRightApiUrl, axiosConfig).then((response) => {
      setAdRight(JSON.parse(response.data.payload));
    });
    Axios.get(adRight2ApiUrl, axiosConfig).then((response) => {
      setAdRight2(JSON.parse(response.data.payload));
    });
    Axios.get(adRight3ApiUrl, axiosConfig).then((response) => {
      setAdRight3(JSON.parse(response.data.payload));
    });

    // Get Home Slider Images
    Axios.get(homeSliderApiUrl, axiosConfig).then((response) => {
      setHomeSlider(JSON.parse(response.data.payload));
    });

    Axios.get(HOME_Right_SLIDER_API_URL, axiosConfig).then((response) => {
      sethomeRightSlider(JSON.parse(response.data.payload));
    });

    Axios.get(footerMenuApiUrl, axiosConfig).then((response) => {
      setFooterMenu(JSON.parse(response.data.payload));
    });

    Axios.get(configData.SETTING_URL, axiosConfig).then((response) => {
      setSettingData(JSON.parse(response.data.payload)[0]);
    });

  }, []);

  
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route
            index
            element={
              <Home
                homeRightSlider={homeRightSlider}
                menus={menus}
                marqueeNews={marqueeNews}
                site_lang = {site_lang}
                adHeader={adHeader}
                adRight={adRight}
                adRight2={adRight2}
                adRight3={adRight3}
                adFooter={adFooter}
                homeSlider={homeSlider}
                todayDate={todayDate}
                footerMenu={footerMenu}
                settingData={settingData}
              />
            }
          />
          <Route
            path="features"
            element={<Features
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang}
            />}
          />
          <Route
            path="support"
            element={<Support
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang}
            />}
          />
          <Route
            path="advertise"
            element={<Advertise
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang}
            />}
          />
          <Route
            path="privacy_policy"
            element={<Privacy_Policy
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang}
            />}
          />
          <Route
            path="about"
            element={<About
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang} />}
              
          />
          <Route
            path="category/:categorySlug"
            element={<CategoryPage
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData} 
              site_lang = {site_lang}
              />}
          />
          <Route
            path="category/:categorySlug/:offsetParam"
            element={<CategoryPage
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData} 
              site_lang = {site_lang}
              />}
          />
          <Route
            path="state/:stateslug"
            element={<State
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              homeSlider={homeSlider}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang}
               />}
          />
          <Route
            path="news-search/"
            element={<CategoryPage
              adHeader={adHeader}
              adFooter={adFooter}
              adRight={adRight}
              adRight2={adRight2}
              adRight3={adRight3}
              todayDate={todayDate}
              menus={menus}
              marqueeNews={marqueeNews}
              footerMenu={footerMenu}
              settingData={settingData}
              site_lang = {site_lang}
               />}
          />
          <Route
            path="details/:postSlug"
            element={
              <CategoryDetailPage
                menus={menus}
                marqueeNews={marqueeNews}
                adHeader={adHeader}
                adFooter={adFooter}
                adRight={adRight}
                adRight2={adRight2}
                adRight3={adRight3}
                todayDate={todayDate}
                footerMenu={footerMenu}
                settingData={settingData}
                site_lang = {site_lang}
              />
            }
          />
          <Route
            path="contact"
            element={
              <ContactUs
                menus={menus}
                marqueeNews={marqueeNews}
                adHeader={adHeader}
                adFooter={adFooter}
                adRight={adRight}
                adRight2={adRight2}
                adRight3={adRight3}
                todayDate={todayDate}
                footerMenu={footerMenu}
                settingData={settingData}
                site_lang = {site_lang}
              />
            }
          />
        </Routes>

      </BrowserRouter>
    </div>
  );

}
export default App;
