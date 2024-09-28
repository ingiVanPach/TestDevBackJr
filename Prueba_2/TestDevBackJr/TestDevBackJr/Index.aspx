<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TestDevBackJr.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Assets/css/CodePen.css" rel="stylesheet" />

    <script type="text/javascript">
        // Guardar la posición antes del postback
        window.onbeforeunload = function () {
            sessionStorage.setItem('scrollPosition', window.scrollY);
        };

        // Recuperar la posición después del postback
        window.onload = function () {
            var scrollPosition = sessionStorage.getItem('scrollPosition');
            if (scrollPosition) {
                window.scrollTo(0, scrollPosition);
                sessionStorage.removeItem('scrollPosition'); // Limpiar el almacenamiento
            }
        };
</script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="titular">
                <img src="https://images.unsplash.com/photo-1522202176988-66273c2fd55f?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1502&q=80" alt="Students gathered around a table, laughing" class="bg" />
                <button onclick="location.href='Login.aspx'" class="btntop">Exit</button>
            </div>
            <div class="context">
                <h1>Empleados</h1>
                <center>
                    <p class="line"></p>
                    <div class="crud-container">

                        <div class="form-group">
                            <h2> Nuevo Empleado</h2>

                            <div class="form-row">
                                <label for="txtLogin">Login:</label>
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-row">
                                <label for="txtNombre">Nombre:</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-row">
                                <label for="txtPaterno">Apellido Paterno:</label>
                                <asp:TextBox ID="txtPaterno" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-row">
                                <label for="txtMaterno">Apellido Materno:</label>
                                <asp:TextBox ID="txtMaterno" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-row">
                                <label for="txtSueldo">Sueldo:</label>
                                <asp:TextBox ID="txtSueldo" runat="server" CssClass="form-control" />
                            </div>

                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Empleado" CssClass="btn" OnClick="btnAgregar_Click" />
                        </div>


                        <br />


                        <asp:GridView ID="gvEmpleado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" OnRowEditing="gvEmpleados_RowEditing" OnRowUpdating="gvEmpleados_RowUpdating" OnRowCancelingEdit="gvEmpleados_RowCancelingEdit" DataKeyNames="userId">
                            <Columns>
                                <asp:BoundField DataField="userId" HeaderText="ID" ReadOnly="True" />
                                <asp:BoundField DataField="Login" HeaderText="Login" ReadOnly="True" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" />
                                <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" ReadOnly="True" />

                                <asp:TemplateField HeaderText="Sueldo">
                                    <ItemTemplate>
                                        <%# Eval("Sueldo") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSueldo" runat="server" Text='<%# Bind("Sueldo") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ShowEditButton="True" />
                            </Columns>
                        </asp:GridView>


                        <br />
                        <br />
                        <div class="form-group">
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar Datos" CssClass="btn" OnClick="btnExportar_Click" />
                        </div>
                    </div>
                </center>
            </div>

            <div class="area">
                <ul class="circles">
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div>
        </div>


    </form>
</body>
</html>

