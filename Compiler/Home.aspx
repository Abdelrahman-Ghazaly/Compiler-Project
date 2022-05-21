<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Compiler.Home"  ValidateRequest="false" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">




    <title></title>
    <style type="text/css">
        .textArea{
           display:flex;
           justify-content:center;
           align-items:center;
        }

        .btn {
            display:flex;
            max-width:900px;
            margin:auto;
            justify-content:space-around;
            align-items:center;
            margin-bottom:10px;
        }

        .auto-style1 {
            height: 331px;
            width: 900px;
            margin-top: 0px;
            margin-bottom: 0px;
        }

        .code-area{
            max-width:920px;
            margin:auto;
            position:relative;
        }

        .outPut{
            max-width:920px;
            margin:auto;
            position:relative;
        }

        .line-number-holder {
            position: absolute;
            top:14px;
            left:-6px;
            max-width:8px;
            font-size:15px;
            line-height: 1.5;
        }
        .textarea {
            background: url(http://i.imgur.com/2cOaJ.png);
            background-attachment: local;
            background-repeat: no-repeat;
            padding-left: 35px;
            padding-top: 12px;
            border-color: #ccc;
            font-size: 14px;
            line-height: 1.1;
            resize:vertical;
        }
        h2{
            text-align:center;
        }
    </style>
</head>
<body>
    <form runat="server">

        <div>
            <br />
        </div>
            <div class="btn">
                <asp:Button CssClass="button" ID="Parse" runat="server"  style="text-align: center" Text="Parse" Width="119px" OnClick="Parse_Click" />
                <asp:Button CssClass="button" ID="comment" runat="server" style="text-align: center" Text="Comment" Width="119px" OnClick="comment_Click" />
                <asp:Button CssClass="button" ID="uncomment" runat="server" style="text-align: center" Text="Uncomment" Width="119px" OnClick="uncomment_Click" />
                <asp:Button CssClass="button" ID="scan" runat="server" style="text-align: center" Text="Scan" Width="119px" OnClick="scan_Click" />
                <asp:Button CssClass="button" ID="clear" runat="server" style="text-align: center" Text="Clear" Width="119px" />\
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button CssClass="button" ID="Browse" runat="server" Text="Upload" OnClick="Browse_Click" />
            </div>
        <div class="code-area">
            <div class="line-number-holder" id="line-number-holder">
            </div>
            <div class="textArea">         
                <textarea  id="CodeArea" name="CodeArea" runat="server" class="auto-style1" TextMode="MultiLine"  rows="10" cols="40" ></textarea>
            </div>
        </div>
    <p>
        </p>
        <div class="outPut">
            <div>
                <h2>Scanner Output</h2>
                <asp:GridView ID="ScannerOutPut" runat="server" CellPadding="4" Width="943px" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" >
                    <Columns>
                        <asp:BoundField DataField="Line NO" HeaderText="Line NO" />
                        <asp:BoundField DataField="Lexeme" HeaderText="Lexeme" />
                        <asp:BoundField DataField="Return Token" HeaderText="Return Token" />
                        <asp:BoundField DataField="Lexeme No in Line" HeaderText="Lexeme No in Line" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
            </div>
                <div>
                    <h2>Parser Output</h2>
                    <asp:GridView ID="ParserOutPut" runat="server" CellPadding="4" Width="943px" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" >
                    <Columns>
                        <asp:BoundField DataField="Line NO" HeaderText="Line NO" />
                        <asp:BoundField DataField="Lexeme" HeaderText="Lexeme" />
                        <asp:BoundField DataField="Matchability" HeaderText="Matchability" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    </asp:GridView>
                </div>
        </div>

    </form>
    
   <script>
       const textarea = document.getElementById('CodeArea');
       const commentBtn = document.getElementById('comment');
       const uncommentBtn = document.getElementById('uncomment');
       const clear = document.getElementById('clear');

       const isSelectionInTextArea = (selectedText) => {
           const code = textarea.value

           return code.includes(selectedText) ? true : false;
       }

       clear.addEventListener("click", () => {
           textarea.value = "";
       })


       commentBtn.addEventListener('click', (e) => {
           e.preventDefault();
           const selectedText = document.getSelection().toString();
           let code = textarea.value

           if (isSelectionInTextArea(selectedText)) {
               let lines = selectedText.split("\n").map(line => {
                   return "***".concat(" ", line);
               });
               lines = lines.join("\n");
               code = code.replace(selectedText, lines);
               textarea.value = code;
           }
           
       })


       uncommentBtn.addEventListener('click', (e) => {
           e.preventDefault();        
           const selectedText = document.getSelection().toString();
           let code = textarea.value;

           if (isSelectionInTextArea(selectedText)) {
               let lines = selectedText.split("\n").map(line => {
                   if (line.includes("***")) {
                       return line.replace("***", "");
                   }
               });
               lines = lines.join("\n");
               code = code.replace(selectedText, lines);
               textarea.value = code;
           }
       })
       
   </script>
    </body>
</html>
