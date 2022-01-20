<?php
include '../../Config/oldDB.php';

$Order_ID = isset($_GET['ODID']) ? $_GET['ODID'] : '0';
$Cus_ID = isset($_GET['CUSID']) ? $_GET['CUSID'] : '0';

$num = 1;
$Total_Price = 0;
$SQLOrderDT = "SELECT * FROM `orders`, `orderdetails`, `product` WHERE `orders`.`Ord_ID` = `orderdetails`.`Ord_ID` AND `orderdetails`.`Pro_ID` = `product`.`Pro_ID` AND `orders`.`Ord_ID` = '$Order_ID' AND `orders`.`Use_ID` = '$Cus_ID'";
$ODDT = $con->query($SQLOrderDT);

function caculateFinalPrice($price, $sale)
{
    if ($sale != 0) {
        return $price * (100 - $sale) / 100;
    }
    return $price;
}

?>

<div style="text-align: center; font-family: 'Poppins', sans-serif;">
    <h1>Order ID: <?= $Order_ID ?></h1>
    <table width="100%" border="1" align="center" cellspacing="0" style="text-align: center;">
        <tr class="table-title">
            <th>STT</th>
            <th>Ảnh</th>
            <th>Tên sản phẩm</th>
            <th>Giá gốc</th>
            <th>Giảm giá</th>
            <th>SL</th>
            <th>Giá cuối</th>
            <th>Thành Tiền</th>
        </tr>

        <?php
        while ($row = $ODDT->fetch_array()) {
        ?>
            <tr>
                <td>
                    <?= $num++ ?>
                </td>
                <td>
                    <a><img style="width: 40%; height: 40%; padding: 0;" src="" alt="img"></a>
                </td>
                <td>
                    <?= $row['Pro_Name'] ?>
                </td>
                <td>
                    <?= number_format($row['Pro_Price'], 0, ",", ".") ?>
                </td>
                <td>
                    <small><strong><?= $row['Pro_Sale'] ?>%</strong></small>
                </td>
                <td>
                    <?= $row['Ode_Amount'] ?>
                </td>
                <td>
                    <?= number_format(caculateFinalPrice((int)$row['Pro_Price'], (int)$row['Pro_Sale']), 0, ",", ".") ?>
                </td>
                <td>
                    <?php
                    $Total = caculateFinalPrice((int)$row['Pro_Price'], (int)$row['Pro_Sale']) * $row['Ode_Amount'];
                    $Total_Price += $Total;
                    ?>
                    <?= number_format($Total, 0, ",", ".") ?> <small><strong><u>VNĐ</u></strong></small>
                </td>
            </tr>
        <?php
        }
        ?>
        <tr>
            <th colspan="7">Tổng Tiền</th>
            <th><?= number_format($Total_Price, 0, ",", ".") ?> <small><strong><u>VNĐ</u></strong></small></th>
        </tr>
    </table>
</div>