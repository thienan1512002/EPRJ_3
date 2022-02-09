import { React, useState, useEffect } from "react";
import { Link, Redirect } from "react-router-dom";
import { Modal, Button, Form } from "react-bootstrap";
import axios from "axios";

const initialUserValue = {
  accountId: "",
  username: "",
  email: "",
  role: "",
  imgSrc:'logo192.png',
  imageFile: null,
};

function User() {
  const [user, setUser] = useState(initialUserValue);
  const [data, setData] = useState(null);
  const [filter, setFilter] = useState("");
  const [show, setShow] = useState(false);
  const [createShow, setCreateShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleCreateClose = () => setCreateShow(false);
  const handleShow = () => setShow(true);
  const handleCreateShow = () => setCreateShow(true);
  useEffect(() => {
    const url = "http://localhost:58017/api/StaffAccounts/";
    axios.get(url).then((data) => {
      //console.log(data.data);
      setData(data.data);
    });
  });
  if (localStorage.getItem("myData") === null) {
    localStorage.setItem("warning", "You have to be login first !");
    return <Redirect to="/" />;
  }
  const handleFilter = (event) => {
    setFilter(event.target.value);
  };
  const userValue = (id, username, email, role) => {
    setUser({
      ...user,
      accountId: id,
      username: username,
      email: email,
      role: role,
    });
    setShow(true);
    console.log(user);
  };
  const showPreview = (event) => {
    if (event.target.files && event.target.files[0]){
      const imageFile = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) =>{
        setUser({
          ...user,
          imageFile:imageFile,
          imgSrc : x.target.result
        })
      }
      reader.readAsDataURL(imageFile);
    }
  }
  const handleSubmit = (event) => {
    event.preventDefault();
    const requestParams = {
      method: "POST",
      body: JSON.stringify({
        accountId : event.target.accountId.value ,
        username : event.target.username.value,
        email : event.target.email.value,
        role : event.target.role.value,
      }),
      headers: { "Content-Type": "application/json" },
    };
    return fetch(
      "http://localhost:58017/api/StaffAccounts/",
      requestParams
    ).then();

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
                      onChange={handleFilter}
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
            <span className="brand-text font-weight-light">AdminLTE 3</span>
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
                  <h1 className="m-0">Account Management</h1>
                  <button
                    className="btn btn-primary m-0"
                    onClick={handleCreateShow}
                  >
                    Add New Account
                  </button>
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
                <table className="table table-hover">
                  <thead className="thead-dark">
                    <tr>
                      <th align="center">Account Id</th>
                      <th align="center">Username</th>
                      <th align="center">Avatar</th>
                      <th align="center">Email</th>
                      <th align="center">Role</th>
                    </tr>
                  </thead>
                  <tbody>
                    {data &&
                      data
                        .filter((item) => item.username.includes(filter))
                        .map((item) => {
                          return (
                            <tr
                              onClick={() =>
                                userValue(
                                  item.accountId,
                                  item.username,
                                  item.email,
                                  item.role
                                )
                              }
                              style={{ cursor: "pointer" }}
                            >
                              <td key={item.accountId}>{item.accountId}</td>
                              <td>{item.username}</td>
                              <td>
                                <img src={item.image} alt={item.image} />
                              </td>
                              <td>{item.email}</td>
                              <td>{item.role}</td>
                            </tr>
                          );
                        })}
                  </tbody>
                </table>
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
      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header>
          <Modal.Title>User Information</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div class="row">
            <div class="col-4">
              <label>Account Id</label>
              <input
                type="text"
                className="form-control"
                readOnly
                value={user.accountId}
              />
            </div>
            <div class="col-8">
              <label>Account Name</label>
              <input
                type="text"
                className="form-control"
                value={user.username}
              />
            </div>
          </div>
          <div class="row">
            <div class="col-8">
              <label>Emaih</label>
              <input type="text" className="form-control" value={user.email} />
            </div>
            <div class="col-4">
              <label>Role</label>
              <input type="text" className="form-control" value={user.role} />
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal
        show={createShow}
        onHide={handleCreateClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header>
          <Modal.Title>User Information</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <img src={user.imgSrc} width="200" height="150" />
              <input type="file" className="form-control-file"  onChange={showPreview}/>
            </div>
            <div className="row">
              <div className="col-4">
                <label>Account Id</label>
                <input type="text" className="form-control" name="accountId" />
              </div>
              <div className="col-8">
                <label>Account Name</label>
                <input type="text" className="form-control" name="username" />
              </div>
            </div>
            <div class="row">
              <div className="col-8">
                <label>Email</label>
                <input type="text" className="form-control" name="email"/>
              </div>
              <div className="col-4">
                <label>Role</label>
                <input type="text" className="form-control" name="role" />
              </div>
            </div>
            <br></br>
            <div className="row">
              <div className="col-4">
                <input type="submit" className="btn btn-primary" value="Add"/>
              </div>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCreateClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}
export default User;
