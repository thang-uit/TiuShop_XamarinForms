<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Product.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$product = new Product($db->connect());

$option = isset($_GET["option"]) && !empty($_GET["option"]) ? $_GET["option"] : "";
$amount = isset($_GET["amount"]) && !empty($_GET["amount"]) ? $_GET["amount"] : "5";

$arrayProduct = $product->getGroupProduct($option, $amount);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProduct,
    "message" => "Product"
);

echo json_encode($array);
