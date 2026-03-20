import React from 'react';
import { Route, Redirect } from "react-router-dom";
import AppNavbar from './AppNavbar';
import { useStateValue } from '../../Context/store';


const RutasProtegidas = ({ component: Component, ...rest }) => {
  const [{ sesionUsuario }, dispatch] = useStateValue();
  return (
    <Route
      {...rest}
      render={props => (
        <>
          <AppNavbar />
          {sesionUsuario ? (
            sesionUsuario.autenticado === true ? (
              <Component {...props} {...rest} />
            ) : (
              <Redirect to="/auth/login" />
            )
          ) : (
            <Redirect to="/auth/login" />
          )}
        </>
      )}
    />
  );
};

export default RutasProtegidas;