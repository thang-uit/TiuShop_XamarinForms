<?php

class Order
{
    public $conn;

    public $orderID = "orderID";
    public $userID = "userID";
    public $name = "name";
    public $email = "email";
    public $phone = "phone";
    public $address = "address";
    public $note = "note";
    public $payment = "payment";
    public $date = "date";
    public $dateSuccess = "dateSuccess";
    public $status = "status";
    public $totalPrice = "totalPrice";

    public $product = "product";

    public $productID = "productID";
    public $pName = "name";
    public $image = "image";
    public $price = "price";
    public $sale = "sale";
    public $isSale = "isSale";
    public $finalPrice = "finalPrice";
    public $size = "size";
    public $quantity = "quantity";

    public $order0 = "order0";
    public $order1 = "order1";
    public $order2 = "order2";
    public $order3 = "order3";
    public $order4 = "order4";

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function GetOrderInfo($userID, $status)
    {
        $arrayAllOrderInfo = [];
        $arrayOrderInfo = [];
        $arrayProduct = [];

        $query = "SELECT * FROM `orders` WHERE `orders`.`Use_ID` = '$userID' AND `orders`.`Ord_Status` = '$status' ORDER BY `orders`.`Ord_Date` DESC;";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        $stmt->setFetchMode(PDO::FETCH_ASSOC);

        while ($row = $stmt->fetch()) {
            $arrayOrderInfo = array(
                $this->orderID => $row["Ord_ID"],
                $this->userID => $row["Use_ID"],
                $this->name => $row["Ord_Name"],
                $this->email => $row["Ord_Email"],
                $this->phone => $row["Ord_Phone"],
                $this->address => $row["Ord_Address"],
                $this->note => $row["Ord_Note"],
                $this->payment => $row["Ord_Payment"],
                $this->date => date_format(date_create($row["Ord_Date"]), "d/m/Y H:i:s"),
                $this->dateSuccess => date_format(date_create($row["Ord_Date_Success"]), "d/m/Y H:i:s"),
                $this->status => $row["Ord_Status"],
                $this->totalPrice => (int) $row["Ord_Total"],
            );

            $query1 = "SELECT * FROM `orderdetails`, `product` WHERE `orderdetails`.`Pro_ID` = `product`.`Pro_ID` AND `orderdetails`.`Ord_ID` = '" . $row["Ord_ID"] . "';";
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute();
            $stmt1->setFetchMode(PDO::FETCH_ASSOC);

            while ($row1 = $stmt1->fetch()) {
                $query2 = "SELECT `productimg`.`Pim_Img` FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '" . $row1["Pro_ID"] . "';";
                $stmt2 = $this->conn->prepare($query2);
                $stmt2->execute();
                $row2 = $stmt2->fetch(PDO::FETCH_ASSOC);

                array_push($arrayProduct, array(
                    $this->productID => $row1["Pro_ID"],
                    $this->pName => $row1["Pro_Name"],
                    $this->image => array($row2["Pim_Img"]),
                    $this->price => number_format($row1['Pro_Price'], 0, ",", "."),
                    $this->sale => $row1["Pro_Sale"] . "%",
                    $this->isSale => $row1["Pro_Sale"] > 0 ? "True" : "False",
                    $this->finalPrice => number_format($this->caculateFinalPrice($row1['Pro_Price'], $row1["Pro_Sale"]), 0, ",", "."),
                    $this->size => $row1["Pro_Size"],
                    $this->quantity => (int) $row1["Ode_Amount"]
                ));
                $arrayOrderInfo[$this->product] = $arrayProduct;
            }
            array_push($arrayAllOrderInfo, $arrayOrderInfo);
            $arrayProduct = [];
        }

        return $arrayAllOrderInfo;
    }


    public function GetAmountOrder($userID)
    {
        $arrayAmount = array(
            $this->order0 => 0,
            $this->order1 => 0,
            $this->order2 => 0,
            $this->order3 => 0,
            $this->order4 => 0
        );

        $orderStatus0 = 0;
        $orderStatus1 = 0;
        $orderStatus2 = 0;
        $orderStatus3 = 0;
        $orderStatus4 = 0;

        $query = "SELECT * FROM `orders` WHERE `orders`.`Use_ID` = '$userID';";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        $stmt->setFetchMode(PDO::FETCH_ASSOC);

        while ($row = $stmt->fetch()) {
            if ($row["Ord_Status"] == "0") {
                $arrayAmount[$this->order0] = $orderStatus0 = $orderStatus0 + 1;
            }

            if ($row["Ord_Status"] == "1") {
                $arrayAmount[$this->order1] = $orderStatus1 = $orderStatus1 + 1;
            }

            if ($row["Ord_Status"] == "2") {
                $arrayAmount[$this->order2] = $orderStatus2 = $orderStatus2 + 1;
            }

            if ($row["Ord_Status"] == "3") {
                $arrayAmount[$this->order3] = $orderStatus3 = $orderStatus3 + 1;
            }

            if ($row["Ord_Status"] == "4") {
                $arrayAmount[$this->order4] = $orderStatus4 = $orderStatus4 + 1;
            }
        }

        return $arrayAmount;
    }

    public function InsertOrder($userID, $name, $email, $phone, $address, $note, $total)
    {
        $queryInsert = "INSERT INTO `orders` (`Ord_ID`, `Use_ID`, `Ord_Name`, `Ord_Email`, `Ord_Phone`, `Ord_Address`, `Ord_Note`, `Ord_Payment`, `Ord_Date`, `Ord_Date_Success`, `Ord_Status`, `Ord_Total`) VALUES (NULL, ?, ?, ?, ?, ?, ?, '0', current_timestamp(), current_timestamp(), '0', ?);";
        $data = array($userID, $name, $email, $phone, $address, $note, $total);
        $stmt1 = $this->conn->prepare($queryInsert);
        $stmt1->execute($data);

        if ($stmt1) {
            $lastOrderID = $this->conn->lastInsertId();

            $query = "SELECT * FROM `cart` WHERE `cart`.`Use_ID` = '$userID' AND `cart`.`Car_Type` = 1;";
            $stmt2 = $this->conn->prepare($query);
            $stmt2->setFetchMode(PDO::FETCH_ASSOC);
            $stmt2->execute();

            while ($row = $stmt2->fetch()) {
                $queryInsert1 = "INSERT INTO `orderdetails` (`Ord_ID`, `Pro_ID`, `Pro_Size`, `Ode_Amount`) VALUES (?, ?, ?, ?);";
                $data1 = array($lastOrderID, $row["Pro_ID"], $row["Pro_Size"], $row["Car_Amount"]);
                $stmt3 = $this->conn->prepare($queryInsert1);
                $stmt3->execute($data1);
            }

            $queryDelete = "DELETE FROM `cart` WHERE `cart`.`Use_ID` = ? AND `cart`.`Car_Type` = 1;";
            $data2 = array($userID);
            $stmt4 = $this->conn->prepare($queryDelete);
            $stmt4->execute($data2);

            if ($stmt4) {
                return 1; // Success
            } else {
                return 2; // Fail
            }
        } else {
            return 2; // Fail
        }
    }

    public function UpdateOrderStatus($orderID, $status)
    {
        $queryUpdate = "UPDATE `orders` SET `Ord_Status` = ? WHERE `orders`.`Ord_ID` = ?;";
        $data = array($status, $orderID);
        $stmt = $this->conn->prepare($queryUpdate);
        $stmt->execute($data);

        if ($stmt) {
            return 1; // Success
        } else {
            return 2; // Fail
        }
    }

    private function caculateFinalPrice($price, $sale)
    {
        if ($sale != 0) {
            return $price * (100 - $sale) / 100;
        }
        return $price;
    }
}
