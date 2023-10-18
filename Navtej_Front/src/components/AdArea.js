import React, { Fragment } from 'react'

export default function AdArea(props){
    const adsData = props.adHeader;
    const adImage = adsData ? adsData.pAsset.AssetLiveUrl : '';
    const adLink = adsData ? adsData.AdLink : '';
    return(
        <Fragment>
            <section className="top-header">
        <div className="container">
            <div className="row">
                <div className="col-lg-12">
                <a href={adLink} target="_blank"> <img src={adImage} /></a>
                </div>
            </div>
        </div>
        </section>
        </Fragment>
    )

}