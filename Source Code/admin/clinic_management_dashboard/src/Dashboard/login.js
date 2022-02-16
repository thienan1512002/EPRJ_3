import { React } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useHistory } from "react-router-dom";
function Login() {
  let redirect = useHistory();
  const processLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(
        "http://localhost:58017/api/StaffAccounts/process-login?username=" +
          e.target.username.value +
          "&password=" +
          e.target.password.value
      );
      const result = await response.json();
      localStorage.setItem("myData", result.username);
      localStorage.removeItem("warning");
      redirect.push("/admin");
    } catch (error) {
      toast.warning("Invalid username or Password", {
        position: toast.POSITION.BOTTOM_RIGHT,
        theme: "dark",
        autoClose: "5000",
      });
    }
  };

  return (
    <div className="hold-transition login-page">
      <div className="login-box">
        <div className="login-logo">
          <span>
            <b>Health Heroes Clinic</b>
          </span>
        </div>
        <div className="card">
          <div className="card-body login-card-body">
            { localStorage.getItem("warning") !== null ?
              <p className="alert alert-warning" align="center">
                {localStorage.getItem("warning")}
              </p>
              : ""
            }
            <p className="login-box-msg">Sign in to start your work</p>
            <form method="post" onSubmit={processLogin}>
              <div className="input-group mb-3">
                <input
                  type="=text"
                  className="form-control"
                  placeholder="Username"
                  name="username"
                />
                <div className="input-group-append">
                  <div className="input-group-text">
                    <span className="fas fa-user"></span>
                  </div>
                </div>
              </div>
              <div className="input-group mb-3">
                <input
                  type="password"
                  className="form-control"
                  placeholder="Password"
                  name="password"
                />
                <div className="input-group-append">
                  <div className="input-group-text">
                    <span className="fas fa-lock"></span>
                  </div>
                </div>
              </div>
              <div className="row">
                <div className="col-6"></div>
                <div className="col-6">
                  <button type="submit" className="btn btn-primary btn-block">
                    Sign In
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}

export default Login;
