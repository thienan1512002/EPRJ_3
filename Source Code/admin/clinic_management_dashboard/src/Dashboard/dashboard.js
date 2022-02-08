import { React, useState, useEffect } from "react";
import { Redirect } from "react-router-dom";
import { Link } from "react-router-dom";
function Dashboards() {
   if (localStorage.getItem("myData") === null) {
     localStorage.setItem("warning","You have to be login first !");
     return <Redirect to="/" />;
   }
  return (
    <div className="hold-transition sidebar-mini layout-fixed">
      <div className="wrapper">
        <nav className="main-header navbar navbar-expand navbar-white navbar-light">
          <ul className="navbar-nav">
            <li className="nav-item">
              <span
                className="nav-link"
                data-widget="pushmenu"
                href="#"
                role="button"
              >
                <i className="fas fa-bars"></i>
              </span>
            </li>
            <li className="nav-item d-none d-sm-inline-block">
              <span href="index3.html" className="nav-link">
                Home
              </span>
            </li>
            {/* <li className="nav-item d-none d-sm-inline-block">
              <span href="#" className="nav-link">
                Contact
              </span>
            </li> */}
          </ul>

          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <span
                className="nav-link"
                data-widget="navbar-search"
                href="#"
                role="button"
              >
                <i className="fas fa-search"></i>
              </span>
              <div className="navbar-search-block">
                <form className="form-inline">
                  <div className="input-group input-group-sm">
                    <input
                      className="form-control form-control-navbar"
                      type="search"
                      placeholder="Search"
                      aria-label="Search"
                    />
                    <div className="input-group-append">
                      <button className="btn btn-navbar" type="submit">
                        <i className="fas fa-search"></i>
                      </button>
                      <button
                        className="btn btn-navbar"
                        type="button"
                        data-widget="navbar-search"
                      >
                        <i className="fas fa-times"></i>
                      </button>
                    </div>
                  </div>
                </form>
              </div>
            </li>
          </ul>
        </nav>
        <aside className="main-sidebar sidebar-dark-primary elevation-4">
          <a href="index3.html" class="brand-link">
            <img
              src="dist/img/AdminLTELogo.png"
              alt="AdminLTE Logo"
              className="brand-image img-circle elevation-3"
            />
            <span className="brand-text font-weight-light">Hello {localStorage.getItem("myData")}</span>
          </a>

          <div className="sidebar">
            <div className="form-inline">
              <div className="input-group" data-widget="sidebar-search">
                <input
                  className="form-control form-control-sidebar"
                  type="search"
                  placeholder="Search"
                  aria-label="Search"
                />
                <div className="input-group-append">
                  <button className="btn btn-sidebar">
                    <i className="fas fa-search fa-fw"></i>
                  </button>
                </div>
              </div>
            </div>

            <nav className="mt-2">
              <ul
                className="nav nav-pills nav-sidebar flex-column"
                data-widget="treeview"
                role="menu"
                data-accordion="false"
              >
                <li className="nav-item">
                  <Link to="/user">
                    <span className=" nav-link text-white">
                      <i className="nav-icon fas fa-user"></i>
                      User
                    </span>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/equipment-clinic">
                    <span className=" nav-link text-white">
                      <i className="nav-icon fas fa-clinic-medical"></i>
                      Equipment For Clinic
                    </span>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/equipment-ecomerce">
                    <span className=" nav-link text-white">
                      <i className="nav-icon fas fa-clinic-medical"></i>
                      Equipment For Ecomerce
                    </span>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/medicine">
                    <span className=" nav-link text-white">
                      <i className="nav-icon fas fa-pills"></i>
                      Medicine
                    </span>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/course">
                    <span className=" nav-link text-white">
                      <i className="nav-icon fab fa-leanpub"></i>
                      Course
                    </span>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/order">
                    <span className=" nav-link text-white">
                      <i className="nav-icon fas fa-pills"></i>
                      Orders
                    </span>
                  </Link>
                </li>
              </ul>
            </nav>
          </div>
        </aside>
        <div className="content-wrapper">
          <div className="content-header">
            <div className="container-fluid">
              <div className="row mb-2">
                <div className="col-sm-6">
                  <h1 className="m-0">Dashboard</h1>
                </div>
                <div className="col-sm-6">
                  <ol className="breadcrumb float-sm-right">
                    <li className="breadcrumb-item">
                      <span href="#">Home</span>
                    </li>
                    <li className="breadcrumb-item active">Dashboard v1</li>
                  </ol>
                </div>
              </div>
            </div>
          </div>
          <section className="content">
            <div className="container-fluid">
              <div className="row">
                <div className="col-lg-3 col-6">
                  <div className="small-box bg-info">
                    <div className="inner">
                      <h3>150</h3>

                      <p>New Orders</p>
                    </div>
                    <div className="icon">
                      <i className="ion ion-bag"></i>
                    </div>
                    <span href="#" className="small-box-footer">
                      More info <i className="fas fa-arrow-circle-right"></i>
                    </span>
                  </div>
                </div>

                <div className="col-lg-3 col-6">
                  <div className="small-box bg-success">
                    <div className="inner">
                      <h3>53%</h3>

                      <p>Bounce Rate</p>
                    </div>
                    <div className="icon">
                      <i className="ion ion-stats-bars"></i>
                    </div>
                    <span href="#" className="small-box-footer">
                      More info <i className="fas fa-arrow-circle-right"></i>
                    </span>
                  </div>
                </div>

                <div className="col-lg-3 col-6">
                  <div className="small-box bg-warning">
                    <div className="inner">
                      <h3>44</h3>

                      <p>User Registrations</p>
                    </div>
                    <div className="icon">
                      <i className="ion ion-person-add"></i>
                    </div>
                    <span href="#" className="small-box-footer">
                      More info <i className="fas fa-arrow-circle-right"></i>
                    </span>
                  </div>
                </div>

                <div className="col-lg-3 col-6">
                  <div className="small-box bg-danger">
                    <div className="inner">
                      <h3>65</h3>

                      <p>Unique Visitors</p>
                    </div>
                    <div className="icon">
                      <i className="ion ion-pie-graph"></i>
                    </div>
                    <span href="#" className="small-box-footer">
                      More info <i className="fas fa-arrow-circle-right"></i>
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </section>
        </div>
        <footer className="main-footer">
          <strong>
            Copyright &copy; 2014-2021{" "}
            <span href="https://adminlte.io">AdminLTE.io</span>.
          </strong>
          All rights reserved.
          <div className="float-right d-none d-sm-inline-block">
            <b>Version</b> 3.1.0
          </div>
        </footer>
        Control Sidebar
        <aside className="control-sidebar control-sidebar-dark">
          Control sidebar content goes here
        </aside>
      </div>
    </div>
  );
}

export default Dashboards;
