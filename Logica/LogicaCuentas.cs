using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Xml;

namespace Logica
{
    internal class LogicaCuentas : ILogicaCuentas
    {
         //singleton
        //------------------------------------------------
        private static LogicaCuentas _instancia = null;
        private LogicaCuentas() { }

        public static LogicaCuentas GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCuentas();

            return _instancia;
        }

        public void AltaCuenta(Cuenta c)
        {
            try
            {
                //PersistenciaCuentas pc = new PersistenciaCuentas();
                CommServicioRemoting.ServicioCuentas remCuentas = new CommServicioRemoting.ServicioCuentas();
                //IPersistenciaCuentas pc = FabricaPersistencia.getPersistenciaCuentas();
                remCuentas.AltaCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Cuenta> ListarCuentas()
        {
            try
            {
                CommServicioRemoting.ServicioCuentas remCuentas = new CommServicioRemoting.ServicioCuentas();

                return remCuentas.ListarCuentas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Cuenta> ListarCuentasCliente(Cliente c)
        {
            try
            {
                CommServicioRemoting.ServicioCuentas remCuentas = new CommServicioRemoting.ServicioCuentas();


                return remCuentas.BuscarCuentasCliente(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarCuenta(Cuenta c)
        {
            try
            {
                CommServicioRemoting.ServicioCuentas remCuentas = new CommServicioRemoting.ServicioCuentas();


                remCuentas.EliminarCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cuenta BuscarCuenta(Cuenta c)
        {
            try
            {
                CommServicioRemoting.ServicioCuentas remCuentas = new CommServicioRemoting.ServicioCuentas();

                return remCuentas.BuscarCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCuenta(Cuenta c)
        {
            try
            {
                CommServicioRemoting.ServicioCuentas remCuentas = new CommServicioRemoting.ServicioCuentas();


                remCuentas.ModificarCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void RealizarMovimiento(Movimiento m)
        {
            try
            {
                //LogicaCuentas lcuentas = new LogicaCuentas();
                ILogicaCuentas lcuentas = FabricaLogica.getLogicaCuentas();
                
                Cuenta cuenta = lcuentas.BuscarCuenta(m.CUENTA);

                if (cuenta != null)
                {
                    CommServicioRemoting.ServicioMovimientos remMovi = new CommServicioRemoting.ServicioMovimientos();


                    //casos a contemplar:
                    //deposito en pesos cuenta en dolares
                    //deposito en pesos cuenta en pesos
                    //deposito en dolares cuenta en dolares
                    //deposito en dolares cuenta en pesos

                    //controlar saldo:

                    //retiro en pesos cuenta en dolares 
                    //retiro en pesos cuenta en pesos
                    //retiro en dolares cuenta en dolares
                    //retiro en dolares cuenta en pesos

                    ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();
                    Cotizacion c = new Cotizacion();
                    c.FECHA = DateTime.Now;
                    c = lc.BuscarCotizacion(c);
                    decimal montoMovimiento = m.MONTO;

                    if (c != null) //si la cotizacion existe calculo montoMovimiento
                    {
                        if (cuenta.MONEDA == "USD")
                        {
                            //la cuenta esta en dolares y el deposito/retiro se esta haciendo en pesos
                            montoMovimiento = m.MONTO / c.PRECIOVENTA;
                        }
                        else
                        {
                            //la cuenta esta en pesos y el deposito/retiro se esta haciendo en dolares
                            montoMovimiento = m.MONTO * c.PRECIOCOMPRA;
                        }
                    }
                    else //si no existe la cotizacion
                    {
                        throw new ErrorNoExisteCotizacion();
                    }

                    if (m.TIPOMOVIMIENTO == 2) // caso deposito
                    {
                        if (cuenta.MONEDA != m.MONEDA) // caso deposito moneda distinta
                        {
                            cuenta.SALDO = cuenta.SALDO + montoMovimiento; //utilizo el valor calculado
                        }
                        else //caso monedas iguales
                        {
                            cuenta.SALDO = cuenta.SALDO + m.MONTO; //utilizo el valor del movimiento m
                        }
                        lcuentas.ActualizarCuenta(cuenta);
                    }
                    else //caso retiro
                    {
                        if (cuenta.MONEDA != m.MONEDA) //caso monedas distintas
                        {
                            if (montoMovimiento > cuenta.SALDO)
                            {
                                throw new ErrorSaldoInsuficienteParaRetiro();
                            }
                            cuenta.SALDO = cuenta.SALDO - montoMovimiento;
                        }
                        else//caso retiro monedas iguales
                        {
                            if (m.MONTO > cuenta.SALDO)
                            {
                                throw new ErrorSaldoInsuficienteParaRetiro();
                            }
                            cuenta.SALDO = cuenta.SALDO - m.MONTO;
                        }
                        lcuentas.ActualizarCuenta(cuenta);
                    }

                    remMovi.RealizarMovimiento(m);
                }
            }
            catch (ErrorSaldoInsuficienteParaRetiro ex)
            {
                throw ex;
            }
            catch (ErrorSucursalNoExiste ex)
            {
                throw ex;
            }
            catch (ErrorUsuarioNoExiste ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void RealizarTransferencia(Movimiento movOrigen, Movimiento movDestino)
        {
            try
            {
                LogicaCuentas lcuentas = new LogicaCuentas();
                Cuenta cuentaOrigen = lcuentas.BuscarCuenta(movOrigen.CUENTA);
                //PersistenciaMovimientos pc = new PersistenciaMovimientos();
                CommServicioRemoting.ServicioMovimientos remMovi = new CommServicioRemoting.ServicioMovimientos();



                //MOVIMIENTO ORIGEN
                if (cuentaOrigen != null)
                {

                    if (cuentaOrigen.MONEDA != movOrigen.MONEDA)
                    {
                        //OBTENEMOS COTIZACION
                        //--------------------
                        //LogicaCotizacion lc = new LogicaCotizacion();
                        ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();
                        Cotizacion c = new Cotizacion();
                        c.FECHA = DateTime.Now;
                        c = lc.BuscarCotizacion(c);

                        if (c != null)
                        {
                            decimal montoMovimiento;
                            if (cuentaOrigen.MONEDA == "USD")
                            {
                                //la cuenta esta en dolares y el deposito se esta haciendo en pesos
                                montoMovimiento = movOrigen.MONTO / c.PRECIOVENTA;
                            }
                            else
                            {
                                //la cuenta esta en pesos y el deposito se esta haciendo en dolares
                                montoMovimiento = movOrigen.MONTO * c.PRECIOCOMPRA;
                            }

                            //actualizamos el nuevo monto del movimiento;
                            //-------------------------------------------
                            movOrigen.MONTO = montoMovimiento;
                        }
                        else
                        {
                            throw new ErrorNoExisteCotizacion();
                        }
                    }

                    if (movOrigen.TIPOMOVIMIENTO == 1 && movOrigen.MONTO > cuentaOrigen.SALDO)
                    {
                        throw new ErrorSaldoInsuficienteParaRetiro();
                    }
                }


                //MOVIMIENTO DEPOSITO
                Cuenta cuentaDestino = lcuentas.BuscarCuenta(movDestino.CUENTA);
                if (cuentaDestino != null)
                {

                    if (cuentaDestino.MONEDA != movOrigen.MONEDA)
                    {
                        //OBTENEMOS COTIZACION
                        //--------------------
                        //LogicaCotizacion lc = new LogicaCotizacion();
                        ILogicaCotizacion lc = FabricaLogica.getLogicaCotizacion();

                        Cotizacion c = new Cotizacion();
                        c.FECHA = DateTime.Now;
                        c = lc.BuscarCotizacion(c);

                        if (c != null)
                        {
                            decimal montoMovimiento;
                            if (cuentaDestino.MONEDA == "USD")
                            {
                                //la cuenta esta en dolares y el deposito se esta haciendo en pesos
                                montoMovimiento = movDestino.MONTO / c.PRECIOVENTA;
                            }
                            else
                            {
                                //la cuenta esta en pesos y el deposito se esta haciendo en dolares
                                montoMovimiento = movDestino.MONTO * c.PRECIOCOMPRA;
                            }

                            //actualizamos el nuevo monto del movimiento;
                            //-------------------------------------------
                            movDestino.MONTO = montoMovimiento;
                        }
                        else
                        {
                            throw new ErrorNoExisteCotizacion();
                        }
                    }

                 
                    //REALIZO LA TRANSFERENCIA
                    //------------------------
                    movOrigen.SUCURSAL = cuentaOrigen.SUCURSAL;
                    movDestino.SUCURSAL = cuentaDestino.SUCURSAL;

                    remMovi.RealizarTransferencia(movOrigen, movDestino);
                }
            }
            catch (ErrorSaldoInsuficienteParaRetiro ex)
            {
                throw ex;
            }
            catch (ErrorNoExisteCotizacion ex)
            {
                throw ex;
            }
            catch (ErrorSucursalNoExiste ex)
            {
                throw ex;
            }
            catch (ErrorUsuarioNoExiste ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Movimiento> ConsultaMovimientosCuenta(Cuenta c, DateTime d)
        {
            try
            {
                CommServicioRemoting.ServicioMovimientos remMovi = new CommServicioRemoting.ServicioMovimientos();

                return remMovi.ConsultaMovimientos(c, d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
