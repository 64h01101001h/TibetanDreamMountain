<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <!--Determino que el formato se aplica a todo el docuemneto-->
  <xsl:template match="/">
    <!--Creo una etiqueta HTML DIV para poder colocar texto dentro de la pagina-->
    <div style="width:100%;margin:auto">
      <table>
        <tr cellpadding="5px" cellspacing="0px" width="100%" style="background-color:Blue;color:White;width:400px ">
          <td>Num Movimiento</td>
          <td>Fecha</td>
          <td>Moneda</td>
          <td>Monto</td>
        </tr>
        <!--Determino como quiero desplegar cada nodo -->
        <xsl:for-each select="Cuenta/Movimiento" >
          <xsl:sort order="ascending" select="Fecha"/>
          <tr style="background-color:Yellow;color:Black" >
            <td>
              <xsl:value-of select="NumeroMovimiento"/>
            </td>
            <td>
              <xsl:value-of select="Fecha"/>
            </td>
            <td>
              <xsl:value-of select="Moneda"/>
            </td>
            <td>
              <xsl:value-of select="Monto"/>
            </td>
          </tr>
        </xsl:for-each>
      </table>
    </div>
  </xsl:template>
</xsl:stylesheet>
