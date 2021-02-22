package at.htl.entity

data class Schwammal(
    val id: Int,
    val firstName: String,
    val lastName: String,
    val poolEdgeSwimmer: Boolean,
    val schoolSubjects: List<SchoolSubject>
)