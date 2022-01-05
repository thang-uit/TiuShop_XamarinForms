<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Product.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$product = new Product($db->connect());

$userID = isset($_GET["userID"]) && !empty($_GET["userID"]) ? $_GET["userID"] : "";
$productID = isset($_GET["productID"]) && !empty($_GET["productID"]) ? $_GET["productID"] : "";

$arrayProduct = $product->getProductDetail($userID, $productID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProduct,
    "message" => "Product"
);

echo json_encode($array);
