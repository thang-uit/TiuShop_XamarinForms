<?php

class Product
{
    public $conn;

    public $productID = "productID";
    public $name = "name";
    public $image = "image";
    public $price = "price";
    public $sale = "sale";
    public $isSale = "isSale";
    public $finalPrice = "finalPrice";
    public $description = "description";
    public $date = "date";
    public $gender = "gender";
    public $categoryID = "categoryID";
    public $collectionID = "collectionID";
    public $stock = "stock";

    public function __construct($database)
    {
        $this->conn = $database;
    }

    public function getGroupProduct($option, $amount) // new, discount | man, woman, both
    {
        $arrayProduct = [];
        $query = "";

        switch ($option) {
            case "new": {
                    $query = "SELECT * FROM `product` ORDER BY rand(" . date("Ymd") . "), `Pro_Date` DESC LIMIT $amount;";
                    break;
                }

            case "discount": {
                    $query = "SELECT * FROM `product` WHERE `product`.`Pro_Sale` > 0 ORDER BY rand(" . date("Ymd") . "), `Pro_Sale` DESC LIMIT $amount;";
                    break;
                }

            case "man": { // Pro_Gender = 0
                    $query = "SELECT * FROM `product` WHERE `product`.`Pro_Gender` = 0 ORDER BY rand(" . date("Ymd") . ") LIMIT $amount;";
                    break;
                }

            case "woman": { // Pro_Gender = 1
                    $query = "SELECT * FROM `product` WHERE `product`.`Pro_Gender` = 1 ORDER BY rand(" . date("Ymd") . ") LIMIT $amount;";
                    break;
                }

            case "both": { // Pro_Gender = 2
                    $query = "SELECT * FROM `product` WHERE `product`.`Pro_Gender` = 2 ORDER BY rand(" . date("Ymd") . ") LIMIT $amount;";
                    break;
                }

            case "all": {
                    $query = "SELECT * FROM `product` ORDER BY rand();";
                    break;
                }

            default: {
                    $query = "SELECT * FROM `product` ORDER BY rand(" . date("Ymd") . "), `Pro_Date` DESC LIMIT $amount;";
                    break;
                }
        }

        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($row = $stmt->fetch()) {
            $query1 = "SELECT `productimg`.`Pim_Img` FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '" . $row["Pro_ID"] . "';";
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute();
            $row1 = $stmt1->fetch(PDO::FETCH_ASSOC);

            array_push($arrayProduct, array(
                $this->productID => $row["Pro_ID"],
                $this->name => $row["Pro_Name"],
                $this->image => array($row1["Pim_Img"]),
                $this->price => number_format($row['Pro_Price'], 0, ",", "."),
                $this->sale => $row["Pro_Sale"] . "%",
                $this->isSale => $row["Pro_Sale"] > 0 ? "True" : "False",
                $this->finalPrice => number_format($this->caculateFinalPrice($row['Pro_Price'], $row["Pro_Sale"]), 0, ",", "."),
                $this->description => $row["Pro_Description"],
                $this->date => $row["Pro_Date"],
                $this->gender => $row["Pro_Gender"],
                $this->categoryID => $row["Cat_ID"],
                $this->collectionID => $row["Col_ID"],
                $this->stock => $row["Pro_Stock"]
            ));
        }

        return $arrayProduct;
    }

    public function getCategoryProduct($categoryID)
    {
        /*
            1 Áo thun
            2 Áo khoác
            3 Áo sơ mi
            4 Quần short
            5 Quần Jean
            6 Quần Jogger
            7 Khác
        */

        $arrayProduct = [];

        $query = "SELECT * FROM `product` WHERE `Cat_ID` = $categoryID";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($row = $stmt->fetch()) {
            $query1 = "SELECT `productimg`.`Pim_Img` FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '" . $row["Pro_ID"] . "';";
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute();
            $row1 = $stmt1->fetch(PDO::FETCH_ASSOC);

            array_push($arrayProduct, array(
                $this->productID => $row["Pro_ID"],
                $this->name => $row["Pro_Name"],
                $this->image => array($row1["Pim_Img"]),
                $this->price => number_format($row['Pro_Price'], 0, ",", "."),
                $this->sale => $row["Pro_Sale"] . "%",
                $this->isSale => $row["Pro_Sale"] > 0 ? "True" : "False",
                $this->finalPrice => number_format($this->caculateFinalPrice($row['Pro_Price'], $row["Pro_Sale"]), 0, ",", "."),
                $this->description => $row["Pro_Description"],
                $this->date => $row["Pro_Date"],
                $this->gender => $row["Pro_Gender"],
                $this->categoryID => $row["Cat_ID"],
                $this->collectionID => $row["Col_ID"],
                $this->stock => $row["Pro_Stock"]
            ));
        }

        return $arrayProduct;
    }

    public function getCollectionsProduct($collectionsID)
    {
        /*
            1 Hangout with friends 
            2 Dating
            3 Party
        */

        $arrayProduct = [];

        $query = "SELECT * FROM `product` WHERE `Col_ID` = $collectionsID ORDER BY rand();";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($row = $stmt->fetch()) {
            $query1 = "SELECT `productimg`.`Pim_Img` FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '" . $row["Pro_ID"] . "';";
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute();
            $row1 = $stmt1->fetch(PDO::FETCH_ASSOC);

            array_push($arrayProduct, array(
                $this->productID => $row["Pro_ID"],
                $this->name => $row["Pro_Name"],
                $this->image => array($row1["Pim_Img"]),
                $this->price => number_format($row['Pro_Price'], 0, ",", "."),
                $this->sale => $row["Pro_Sale"] . "%",
                $this->isSale => $row["Pro_Sale"] > 0 ? "True" : "False",
                $this->finalPrice => number_format($this->caculateFinalPrice($row['Pro_Price'], $row["Pro_Sale"]), 0, ",", "."),
                $this->description => $row["Pro_Description"],
                $this->date => $row["Pro_Date"],
                $this->gender => $row["Pro_Gender"],
                $this->categoryID => $row["Cat_ID"],
                $this->collectionID => $row["Col_ID"],
                $this->stock => $row["Pro_Stock"]
            ));
        }

        return $arrayProduct;
    }

    public function getProductDetail($productID)
    {
        // $query = "SELECT * FROM `product` WHERE `product`.`Pro_ID` = '$productID';";
        $query = "SELECT * FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '$productID';";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);

        $arrayProduct = [];
        $arrayImg = [];

        if ($stmt->rowCount() > 0) {
            $arrayProduct = array(
                $this->productID => $row["Pro_ID"],
                $this->name => $row["Pro_Name"],
                $this->price => number_format($row['Pro_Price'], 0, ",", "."),
                $this->sale => $row["Pro_Sale"] . "%",
                $this->isSale => $row["Pro_Sale"] > 0 ? "True" : "False",
                $this->finalPrice => number_format($this->caculateFinalPrice($row['Pro_Price'], $row["Pro_Sale"]), 0, ",", "."),
                $this->description => $row["Pro_Description"],
                $this->date => $row["Pro_Date"],
                $this->gender => $row["Pro_Gender"],
                $this->categoryID => $row["Cat_ID"],
                $this->collectionID => $row["Col_ID"],
                $this->stock => $row["Pro_Stock"]
            );
            array_push($arrayImg, $row["Pim_Img"]);

            while ($row1 = $stmt->fetch(PDO::FETCH_ASSOC)) {
                array_push($arrayImg, $row1["Pim_Img"]);
            }
            $arrayProduct[$this->image] = $arrayImg;
        } else {
            $arrayProduct = null;
        }
        return $arrayProduct;
    }

    public function searchProduct($keyword)
    {
        $arrayProduct = [];

        $query = "SELECT * FROM `product` WHERE LOWER(Pro_Name) LIKE '%$keyword%' OR LOWER(Pro_Name) LIKE '%$keyword%'";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($row = $stmt->fetch()) {
            $query1 = "SELECT `productimg`.`Pim_Img` FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '" . $row["Pro_ID"] . "';";
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute();
            $row1 = $stmt1->fetch(PDO::FETCH_ASSOC);

            array_push($arrayProduct, array(
                $this->productID => $row["Pro_ID"],
                $this->name => $row["Pro_Name"],
                $this->image => array($row1["Pim_Img"]),
                $this->price => number_format($row['Pro_Price'], 0, ",", "."),
                $this->sale => $row["Pro_Sale"] . "%",
                $this->isSale => $row["Pro_Sale"] > 0 ? "True" : "False",
                $this->finalPrice => number_format($this->caculateFinalPrice($row['Pro_Price'], $row["Pro_Sale"]), 0, ",", "."),
                $this->description => $row["Pro_Description"],
                $this->date => $row["Pro_Date"],
                $this->gender => $row["Pro_Gender"],
                $this->categoryID => $row["Cat_ID"],
                $this->collectionID => $row["Col_ID"],
                $this->stock => $row["Pro_Stock"]
            ));
        }

        return $arrayProduct;
    }

    private function caculateFinalPrice($price, $sale)
    {
        if ($sale != 0) {
            return $price * (100 - $sale) / 100;
        }
        return $price;
    }
}
