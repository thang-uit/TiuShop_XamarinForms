<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Product.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$product = new Product($db->connect());

/*
    1 Áo thun
    2 Áo khoác
    3 Áo sơ mi
    4 Quần short
    5 Quần Jean
    6 Quần Jogger
    7 Khác
*/

$categoryID = isset($_GET["categoryID"]) && !empty($_GET["categoryID"]) ? $_GET["categoryID"] : "1";

$arrayProduct = $product->getCategoryProduct($categoryID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProduct,
    "message" => "Get product from a category"
);

echo json_encode($array);
